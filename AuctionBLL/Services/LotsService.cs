using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionBLL.ViewModels;
using AuctionDAL.Exceptions;
using AuctionDAL.Models;
using AuctionDAL.Repositories;
using AutoMapper;

namespace AuctionBLL.Services
{
    public class LotsService : ILotsService
    {
        private readonly ILotsRepository _repository;
        private readonly IMapper _mapper;

        public LotsService(ILotsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        private IEnumerable<LotViewModel> MapLotsToViewModels(IEnumerable<Lot> unmappedItems) => _mapper.Map<IEnumerable<Lot>, IEnumerable<LotViewModel>>(unmappedItems);
        
        private LotViewModel MapLotToViewModel(Lot unmapped) => _mapper.Map<Lot, LotViewModel>(unmapped);

        private Lot MapLotViewModelToLot(LotViewModel unmapped) => _mapper.Map<LotViewModel, Lot>(unmapped);

        public async Task<IEnumerable<LotViewModel>> GetAllLotsAsync()
        {
            var unmappedItems = await _repository.GetAllLotsAsync();

            var mappedItems = MapLotsToViewModels(unmappedItems);

            return mappedItems;
        }

        public async Task<IEnumerable<LotViewModel>> GetAllOpenedLotsAsync()
        {
            var unmappedItems = await _repository.GetAllOpenLotsAsync();

            var mappedItems = MapLotsToViewModels(unmappedItems);

            return mappedItems;
        }

        public async Task<IEnumerable<LotViewModel>> GetAllClosedLotsAsync()
        {
            var unmappedItems = await _repository.GetAllClosedLotsAsync();

            var mappedItems = MapLotsToViewModels(unmappedItems);

            return mappedItems;
        }

        public async Task<LotViewModel> GetLotByIdAsync(Guid id)
        {
            try
            {
                var unmappedItem = await _repository.GetLotByIdAsync(id);

                var mappedItem = MapLotToViewModel(unmappedItem);

                return mappedItem;
            }
            catch (ItemNotFoundException)
            {
                throw;
            }
        }

        public async Task<LotViewModel> CreateLotAsync(LotViewModel lot)
        {
            if (lot is null)
                throw new ArgumentNullException(nameof(lot));
            
            AssertModelIsValid(lot);

            var mapped = MapLotViewModelToLot(lot);
            try
            {
                await _repository.CreateLotAsync(mapped);
            }
            catch (ItemAlreadyExistsException)
            {
                // TODO: ?
            }

            return lot;
        }

        public async Task<LotViewModel> AddParticipantAsync(LotViewModel lot, User user)
        {
            if (lot is null)
                throw new ArgumentNullException(nameof(lot));
            if (user is null)
                throw new ArgumentNullException(nameof(user));
            if (lot.Participants.Contains(user))
                throw new InvalidOperationException("User already participant");
            
            lot.Participants.Add(user);
            
            // TODO: _logger.AddNote or smth
            
            var mapped = MapLotViewModelToLot(lot);

            await _repository.UpdateLotAsync(mapped);
            
            return lot;
        }

        public async Task<LotViewModel> SetLotActualPriceAsync(LotViewModel lot, User user, MoneyViewModel newPrice)
        {
            if (lot is null)
                throw new ArgumentNullException(nameof(lot));
            if (user is null)
                throw new ArgumentNullException(nameof(user));
            if (newPrice is null)
                throw new ArgumentNullException(nameof(newPrice));
            if (newPrice < lot.ActualPrice)
                throw new InvalidOperationException("New price lower than actual");
            if (lot.Participants.Contains(user) == false)
                throw new InvalidOperationException("User is not a participant of the lot");

            lot.ActualPrice = newPrice;
            
            // TODO: _logger.AddNote or smth
            
            var mapped = MapLotViewModelToLot(lot);
            
            await _repository.UpdateLotAsync(mapped);

            return lot;
        }

        public async Task<LotViewModel> OpenLotAsync(LotViewModel lot)
        {
            if (lot is null)
                throw new ArgumentNullException(nameof(lot));
            if (lot.IsOpen)
                throw new InvalidOperationException("Lot is already opened");

            lot.IsOpen = true;

            // TODO: _logger.AddNote or smth
            
            var mapped = MapLotViewModelToLot(lot);

            await _repository.UpdateLotAsync(mapped);

            return lot;
        }

        public async Task<LotViewModel> CloseLotAsync(LotViewModel lot)
        {
            if (lot is null)
                throw new ArgumentNullException(nameof(lot));
            if (lot.IsOpen == false)
                throw new InvalidOperationException("Lot is already closed");

            lot.IsOpen = false;
            
            // TODO: _logger.AddNote or smth

            var mapped = MapLotViewModelToLot(lot);

            await _repository.UpdateLotAsync(mapped);

            return lot;
        }

        private void AssertModelIsValid(LotViewModel lot)
        {
            if (String.IsNullOrEmpty(lot.Name)
                || String.IsNullOrEmpty(lot.Description)
                || lot.Owner is null
                || lot.StartPrice is null)
                throw new ArgumentException($"Argument parameters is not correct", nameof(lot));
        }
    }
}
