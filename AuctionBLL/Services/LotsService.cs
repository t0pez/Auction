﻿using AuctionBLL.Dto;
using AuctionBLL.Enums;
using AuctionDAL.Exceptions;
using AuctionDAL.Models;
using AuctionDAL.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionDAL;

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

        private IEnumerable<LotDto> MapLotsToViewModels(IEnumerable<Lot> unmappedItems) =>
            _mapper.Map<IEnumerable<Lot>, IEnumerable<LotDto>>(unmappedItems);
        
        private LotDto MapLotToViewModel(Lot unmapped) => _mapper.Map<Lot, LotDto>(unmapped);

        private Lot MapLotViewModelToLot(LotDto unmapped) => _mapper.Map<LotDto, Lot>(unmapped);

        public async Task<IEnumerable<LotDto>> GetAllLotsAsync()
        {
            var unmappedItems = await _unitOfWork.LotsRepository.GetAllLotsAsync();

            var mappedItems = MapLotsToViewModels(unmappedItems);

            return mappedItems;
        }

        public async Task<IEnumerable<LotDto>> GetAllCreatedLotsAsync()
        {
            var unmappedItems = await _unitOfWork.LotsRepository.GetLotsByPredicateAsync(lot => lot.Status == (int) LotStatus.Created);

            var mappedItems = MapLotsToViewModels(unmappedItems);

            return mappedItems;
        }

        public async Task<IEnumerable<LotDto>> GetAllOpenedLotsAsync()
        {
            var unmappedItems = await _unitOfWork.LotsRepository.GetLotsByPredicateAsync(lot => lot.Status == (int) LotStatus.Opened);

            var mappedItems = MapLotsToViewModels(unmappedItems);

            return mappedItems;
        }

        public async Task<IEnumerable<LotDto>> GetAllClosedLotsAsync()
        {
            var unmappedItems = await _unitOfWork.LotsRepository.GetLotsByPredicateAsync(lot => lot.Status == (int) LotStatus.Closed);

            var mappedItems = MapLotsToViewModels(unmappedItems);

            return mappedItems;
        }

        public async Task<LotDto> GetLotByIdAsync(Guid id)
        {
            try
            {
                var unmappedItem = await _unitOfWork.LotsRepository.GetLotByIdAsync(id);

                var mappedItem = MapLotToViewModel(unmappedItem);

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

            var mapped = MapLotViewModelToLot(lot);
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

        public async Task<LotDto> AddParticipantAsync(LotDto lot, UserDto user)
        {
            if (lot is null)
                throw new ArgumentNullException(nameof(lot));
            if (user is null)
                throw new ArgumentNullException(nameof(user));
            if (lot.Participants.Contains(user))
                throw new InvalidOperationException("User already participant");
            
            lot.Participants.Add(user);
            
            var mapped = MapLotViewModelToLot(lot);

            try
            {
                await _unitOfWork.LotsRepository.UpdateLotAsync(mapped);
                
                // TODO: _logger.AddNote or smth
            }
            catch (ItemNotFoundException)
            {
                throw;
            }

            return lot;
        }

        public async Task<LotDto> SetLotActualPriceAsync(LotDto lot, UserDto user, MoneyDto newPrice)
        {
            if (lot is null)
                throw new ArgumentNullException(nameof(lot));
            if (user is null)
                throw new ArgumentNullException(nameof(user));
            if (newPrice is null)
                throw new ArgumentNullException(nameof(newPrice));
            if (newPrice < lot.ActualPrice)
                throw new InvalidOperationException("New price can not be lower than actual");
            if (lot.Participants.Contains(user) == false)
                throw new InvalidOperationException("User is not a participant of the lot");

            lot.ActualPrice = newPrice;
            
            // TODO: _logger.AddNote or smth
            
            var mapped = MapLotViewModelToLot(lot);
            
            await _unitOfWork.LotsRepository.UpdateLotAsync(mapped);

            return lot;
        }

        public async Task<LotDto> OpenLotAsync(LotDto lot)
        {
            if (lot is null)
                throw new ArgumentNullException(nameof(lot));
            if (lot.Status == LotStatus.Opened)
                throw new InvalidOperationException("Lot is already opened");

            lot.Status = LotStatus.Opened;

            // TODO: sub to event when needs to close
            
            // TODO: _logger.AddNote or smth
            
            var mapped = MapLotViewModelToLot(lot);

            await _unitOfWork.LotsRepository.UpdateLotAsync(mapped);

            return lot;
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
