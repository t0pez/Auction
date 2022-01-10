using AuctionBLL.Dto;
using AuctionBLL.Enums;
using AuctionBLL.Extensions.Dto;
using AuctionBLL.Interfaces;
using AuctionDAL;
using AuctionDAL.Exceptions;
using AuctionDAL.Extensions.Models;
using AuctionDAL.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionBLL.Services
{
    public class LotsService : ILotsService, IDisposable
    {
        private readonly ITimeEventService<Guid> _openTimeEventService;
        private readonly ITimeEventService<Guid> _closeTimeEventService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LotsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _openTimeEventService = new TimeEventService<Guid>();
            _closeTimeEventService = new TimeEventService<Guid>();

            _openTimeEventService.TimeToInvoke += OpenLotAsync;
            _closeTimeEventService.TimeToInvoke += CloseLotAsync;

            InitializeListeners();
        }
        
        public async Task<IEnumerable<LotDto>> GetAllLotsAsync()
        {
            var unmappedItems = await _unitOfWork.LotsRepository.GetAllLotsAsync();

            var mappedItems = _mapper.Map<IEnumerable<LotDto>>(unmappedItems);

            return mappedItems;
        }

        public async Task<LotDto> GetLotByIdAsync(Guid id)
        {
            try
            {
                var unmappedItem = await _unitOfWork.LotsRepository.GetLotByIdAsync(id);

                var mappedItem = _mapper.Map<LotDto>(unmappedItem);

                return mappedItem;
            }
            catch (ItemNotFoundException e)
            {
                throw new InvalidOperationException("Lot not found", e);
            }
        }

        public async Task<LotDto> CreateLotAsync(LotDto lotDto, string ownerId)
        {
            if (lotDto is null)
                throw new ArgumentNullException(nameof(lotDto));
            
            AssertModelIsValid(lotDto);

            var owner = await _unitOfWork.UserManager.FindByIdAsync(ownerId);

            if (owner is null)
                throw new InvalidOperationException("User not found");

            if (owner.HasMoneyOfCurrency(lotDto.StartPrice.Currency.Value) == false)
                throw new ArgumentException($"Need wallet with this type of currency {lotDto.StartPrice.Currency.IsoName}");
            
            lotDto.CreatingInitialize();

            var lotModel = _mapper.Map<Lot>(lotDto);
            lotModel.Owner = owner;
            
            await _unitOfWork.LotsRepository.CreateLotAsync(lotModel);
            await _unitOfWork.SaveChangesAsync();
            
            _openTimeEventService.Add(lotDto.Id, (DateTime) lotDto.StartDate);

            return lotDto;
        }

        public async Task<LotDto> AddParticipantAsync(Guid lotId, string userId)
        {
            try
            {
                var lot = await _unitOfWork.LotsRepository.GetLotByIdAsync(lotId);
                var user = await _unitOfWork.UserManager.FindByIdAsync(userId);

                if (lot.Status is (int) LotStatus.Closed or (int) LotStatus.Cancelled)
                    throw new InvalidOperationException("Can not add participant to closed or cancelled lot");
                if (lot.Participants.Contains(user))
                    throw new InvalidOperationException("User is already a participant");
                if (user.HasMoneyOfCurrency(lot.StartPrice.Currency) == false)
                    throw new ArgumentException($"Need wallet with this type of currency {lot.StartPrice.Currency}");

                lot.Participants.Add(user);
                user.LotsAsParticipant.Add(lot);

                var lotUpdateTask = _unitOfWork.LotsRepository.UpdateLotAsync(lot);
                var userUpdateTask = _unitOfWork.UserManager.UpdateAsync(user);

                await Task.WhenAll(lotUpdateTask, userUpdateTask);

                await _unitOfWork.SaveChangesAsync();

                var mapped = _mapper.Map<LotDto>(lot);

                return mapped;

            }
            catch (ItemNotFoundException e)
            {
                throw new InvalidOperationException("Lot not found", e);
            }
        }
        
        public async Task<LotDto> SetLotActualPriceAsync(Guid lotId, string userId, decimal newPrice)
        {
            try
            {
                var lot = await _unitOfWork.LotsRepository.GetLotByIdAsync(lotId);
                var user = await _unitOfWork.UserManager.FindByIdAsync(userId);
                var lotCurrency = lot.StartPrice.Currency;

                if (lot.Status is (int) LotStatus.Closed or (int) LotStatus.Cancelled)
                    throw new InvalidOperationException("Can not set new price to closed or cancelled lot");
                if (lot.Acquirer is not null && lot.Acquirer.Id == user.Id)
                    throw new InvalidOperationException("User already leads lot. He can not make step");
                if (newPrice < lot.HighestPrice.Amount + lot.MinStepPrice.Amount)
                    throw new ArgumentException("New price can not be lower than actual plus minimal step");
                if (lot.Participants.Contains(user) == false)
                    throw new ArgumentException("User is not a participant of the lot");
                if (user.HasMoneyOfCurrency(lotCurrency) == false)
                    throw new ArgumentException($"Need wallet with this type of currency {lotCurrency}");
                if (user.HasEnoughMoneyOfCurrency(lotCurrency, newPrice) == false)
                    throw new ArgumentException("Not enough money");
                
                lot.HighestPrice.Amount = newPrice;
                lot.Acquirer = user;
                lot.EndDate += lot.ProlongationTime;

                await _unitOfWork.LotsRepository.UpdateLotAsync(lot);
                await _unitOfWork.SaveChangesAsync();

                _closeTimeEventService.Prolong(lot.Id, lot.ProlongationTime);

                var mapped = _mapper.Map<LotDto>(lot);

                return mapped;
            }
            catch (ItemNotFoundException e)
            {
                throw new InvalidOperationException("Lot not found", e);
            }
        }

        public async Task OpenLotAsync(Guid lotId)
        {
            try
            {
                var lot = await _unitOfWork.LotsRepository.GetLotByIdAsync(lotId);

                if (lot.Status == (int) LotStatus.Opened)
                    throw new InvalidOperationException("Lot is already opened");

                lot.Status = (int) LotStatus.Opened;
                lot.EndDate = DateTime.Now + lot.ProlongationTime;

                await _unitOfWork.LotsRepository.UpdateLotAsync(lot);
                await _unitOfWork.SaveChangesAsync();

                _closeTimeEventService.Add(lot.Id, (DateTime) lot.EndDate);
            }
            catch (ItemNotFoundException e)
            {
                throw new InvalidOperationException("Lot not found", e);
            }
        }

        public async Task CloseLotAsync(Guid id)
        {
            try
            {
                var lot = await _unitOfWork.LotsRepository.GetLotByIdAsync(id);

                if (lot.Status is (int)LotStatus.Closed or (int)LotStatus.Cancelled)
                    throw new InvalidOperationException("Lot is already closed");

                lot.EndDate = DateTime.Now;

                var acquirer = lot.Acquirer;

                if (acquirer is null)
                {
                    lot.Status = (int)LotStatus.Cancelled;
                }
                else
                {
                    lot.Status = (int)LotStatus.Closed;

                    acquirer.AcquiredLots.Add(lot); // TODO: Extract to maybe some kind of a EventManager

                    var acquirerMoney = acquirer.GetMoneyOfCurrency(lot.StartPrice.Currency);
                    acquirerMoney.Amount -= lot.HighestPrice.Amount;

                    var ownerMoney = lot.Owner.GetMoneyOfCurrency(lot.StartPrice.Currency);
                    ownerMoney.Amount += lot.HighestPrice.Amount;

                    await _unitOfWork.UserManager.UpdateAsync(acquirer);
                }

                await _unitOfWork.LotsRepository.UpdateLotAsync(lot);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (ItemNotFoundException e)
            {
                throw new InvalidOperationException("Lot not found", e);
            }
        }

        private void InitializeListeners()
        {
            var createdLots =
                _unitOfWork.LotsRepository.GetLotsByPredicateAsync(lot => lot.Status == (int) LotStatus.Created).GetAwaiter().GetResult();
            var openedLots =
                _unitOfWork.LotsRepository.GetLotsByPredicateAsync(lot => lot.Status == (int) LotStatus.Opened).GetAwaiter().GetResult();

            _openTimeEventService.AddRange(createdLots.Select(lot => (lot.Id, lot.StartDate)));
            _closeTimeEventService.AddRange(openedLots.Select(lot => (lot.Id, lot.StartDate)));
        }

        private void AssertModelIsValid(LotDto lot)
        {
            if (String.IsNullOrEmpty(lot.Name)
                || String.IsNullOrEmpty(lot.Description)
                || lot.StartPrice is null
                || lot.StartPrice.Amount < 0
                || lot.MinStepPrice is null
                || lot.MinStepPrice.Amount < 0)
                throw new ArgumentException("Argument parameters is not correct", nameof(lot));
        }

        public void Dispose()
        {
            _openTimeEventService.TimeToInvoke -= OpenLotAsync;
            _closeTimeEventService.TimeToInvoke -= CloseLotAsync;
        }
    }
}
