using AuctionBLL.Dto;
using AuctionBLL.Enums;
using AuctionDAL;
using AuctionDAL.Exceptions;
using AuctionDAL.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionBLL.Services
{
    public class LotsService : ILotsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LotsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        private IEnumerable<LotDto> MapLotsToDto(IEnumerable<Lot> unmappedItems) =>
            _mapper.Map<IEnumerable<Lot>, IEnumerable<LotDto>>(unmappedItems);
        
        private LotDto MapLotToViewModel(Lot unmapped) => _mapper.Map<Lot, LotDto>(unmapped);

        private Lot MapLotViewModelToLot(LotDto unmapped) => _mapper.Map<LotDto, Lot>(unmapped);

        public async Task<IEnumerable<LotDto>> GetAllLotsAsync()
        {
            var unmappedItems = await _unitOfWork.LotsRepository.GetAllLotsAsync();

            var mappedItems = _mapper.Map<IEnumerable<LotDto>>(unmappedItems);

            return mappedItems;
        }

        public async Task<IEnumerable<LotDto>> GetAllCreatedLotsAsync()
        {
            var unmappedItems = await _unitOfWork.LotsRepository.GetLotsByPredicateAsync(lot => lot.Status == (int) LotStatus.Created);

            var mappedItems = _mapper.Map<IEnumerable<LotDto>>(unmappedItems);

            return mappedItems;
        }

        public async Task<IEnumerable<LotDto>> GetAllOpenedLotsAsync()
        {
            var unmappedItems = await _unitOfWork.LotsRepository.GetLotsByPredicateAsync(lot => lot.Status == (int) LotStatus.Opened);

            var mappedItems = _mapper.Map<IEnumerable<LotDto>>(unmappedItems);

            return mappedItems;
        }

        public async Task<IEnumerable<LotDto>> GetAllClosedLotsAsync()
        {
            var unmappedItems = await _unitOfWork.LotsRepository.GetLotsByPredicateAsync(lot => lot.Status == (int) LotStatus.Closed);

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
            catch (ItemNotFoundException)
            {
                throw;
            }
        }

        public async Task<LotDto> CreateLotAsync(LotDto lot)
        {
            if (lot is null)
                throw new ArgumentNullException(nameof(lot));
            
            AssertModelIsValid(lot);

            var owner = await _unitOfWork.UserManager.FindByIdAsync(lot.Owner.Id);

            if (owner is null)
                throw new InvalidOperationException("User is not authorized");

            lot.Id = Guid.NewGuid();
            lot.Status = LotStatus.Created;
            lot.DateOfCreation = DateTime.Now;
            lot.ActualPrice = lot.StartPrice;

            // TODO: sub to event when needs to open

            var mapped = _mapper.Map<Lot>(lot);
            mapped.Owner = owner;
            mapped.HighestPrice.Id = Guid.NewGuid();
            mapped.StartPrice.Id = Guid.NewGuid();
            mapped.MinStepPrice.Id = Guid.NewGuid();
            mapped.HighestPrice.Currency = mapped.StartPrice.Currency;
            mapped.HighestPrice.Amount = mapped.StartPrice.Amount;

            try
            {
                await _unitOfWork.LotsRepository.CreateLotAsync(mapped);
            }
            catch (ItemAlreadyExistsException)
            {
                // TODO: ?
                throw;
            }

            return lot;
        }

        public async Task<LotDto> AddParticipantAsync(Guid lotId, string userId)
        {
            try
            {
                var lot = await _unitOfWork.LotsRepository.GetLotByIdAsync(lotId);
                var user = await _unitOfWork.UserManager.FindByIdAsync(userId);

                //if(lot.Participants is not null)
                    if (lot.Participants.Contains(user))
                        throw new InvalidOperationException("User is already a participant");

                lot.Participants.Add(user);
                user.LotsAsParticipant.Add(lot);

                var lotUpdate = _unitOfWork.LotsRepository.UpdateLotAsync(lot);
                var userUpdate = _unitOfWork.UserManager.UpdateAsync(user);

                await Task.WhenAll(lotUpdate, userUpdate);

                await _unitOfWork.SaveChangesAsync();

                var mapped = MapLotToViewModel(lot);

                return mapped;
            }
            catch (ItemNotFoundException)
            {
                throw;
            }
        }

        public async Task<LotDto> SetLotActualPriceAsync(Guid lotId, string userId, decimal newPrice)
        {
            if (userId is null)
                throw new ArgumentNullException(nameof(userId));

            try
            {
                var lot = await _unitOfWork.LotsRepository.GetLotByIdAsync(lotId);
                var user = await _unitOfWork.UserManager.FindByIdAsync(userId);

                if (newPrice < lot.HighestPrice.Amount)
                    throw new InvalidOperationException("New price can not be lower than actual");
                if (lot.Participants.Contains(user) == false)
                    throw new InvalidOperationException("User is not a participant of the lot");

                lot.HighestPrice.Amount = newPrice;

                // TODO: _logger.AddNote or smth


                await _unitOfWork.LotsRepository.UpdateLotAsync(lot);

                var mapped = MapLotToViewModel(lot);

                return mapped;
            }
            catch (InvalidOperationException) // TODO: change for custom exception (ValidationException or smth)
            {
                throw;
            }
        }

        public async Task<LotDto> OpenLotAsync(Guid lotId)
        {
            var lot = await _unitOfWork.LotsRepository.GetLotByIdAsync(lotId);
            
            if (lot.Status == (int) LotStatus.Opened)
                throw new InvalidOperationException("Lot is already opened");

            lot.Status = (int) LotStatus.Opened;

            // TODO: sub to event when needs to close
            
            // TODO: _logger.AddNote or smth
            
            await _unitOfWork.LotsRepository.UpdateLotAsync(lot);
            
            var mapped = MapLotToViewModel(lot);

            return mapped;
        }

        public async Task<LotDto> CloseLotAsync(LotDto lot)
        {
            if (lot is null)
                throw new ArgumentNullException(nameof(lot));
            if (lot.Status == LotStatus.Closed)
                throw new InvalidOperationException("Lot is already closed");

            lot.Status = LotStatus.Closed;
            
            // TODO: _logger.AddNote or smth

            var mapped = MapLotViewModelToLot(lot);

            await _unitOfWork.LotsRepository.UpdateLotAsync(mapped);

            return lot;
        }

        private void AssertModelIsValid(LotDto lot)
        {
            if (String.IsNullOrEmpty(lot.Name)
                || String.IsNullOrEmpty(lot.Description)
                || lot.Owner is null
                || lot.StartPrice is null
                || lot.StartPrice.Amount < 0
                || lot.MinStepPrice is null
                || lot.MinStepPrice.Amount < 0)
                throw new ArgumentException($"Argument parameters is not correct", nameof(lot));
        }
    }
}
