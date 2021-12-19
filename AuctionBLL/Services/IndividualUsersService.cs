using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionBLL.Dto;
using AuctionDAL.Exceptions;
using AuctionDAL.Models;
using AuctionDAL.Repositories;
using AutoMapper;

namespace AuctionBLL.Services
{
    public class IndividualUsersService : IIndividualUsersService
    {
        private readonly IUserRepository<IndividualUser> _repository;
        private readonly IMapper _mapper;

        public IndividualUsersService(IUserRepository<IndividualUser> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private IEnumerable<IndividualUserDto> MapModelsToViewModels(IEnumerable<IndividualUser> unmapped) =>
            _mapper.Map<IEnumerable<IndividualUser>, IEnumerable<IndividualUserDto>>(unmapped);

        private IndividualUserDto MapModelToViewModel(IndividualUser unmapped) =>
            _mapper.Map<IndividualUser, IndividualUserDto>(unmapped);
        
        private IndividualUser MapViewModelToModel(IndividualUserDto unmapped) =>
            _mapper.Map<IndividualUserDto, IndividualUser>(unmapped);

        
        public async Task<IEnumerable<IndividualUserDto>> GetAllUsersAsync()
        {
            var unmapped = await _repository.GetAllUsersAsync();

            var mapped = MapModelsToViewModels(unmapped);

            return mapped;
        }

        public async Task<IndividualUserDto> GetUserByIdAsync(Guid id)
        {
            try
            {
                var unmapped = await _repository.GetUserByIdAsync(id);

                var mapped = MapModelToViewModel(unmapped);

                return mapped;
            }
            catch (ItemNotFoundException)
            {
                // TODO
                return new IndividualUserDto();
            }
        }

        public async Task<IndividualUserDto> CreateUserAsync(IndividualUserDto newUser)
        {
            if (newUser is null)
                throw new ArgumentNullException(nameof(newUser));
            
            AssertModelIsCorrect(newUser);

            var mapped = MapViewModelToModel(newUser);
            try
            {
                await _repository.CreateUserAsync(mapped);
            }
            catch (ItemAlreadyExistsException)
            {
                // TODO
            }

            return newUser;
        }

        public async Task<IndividualUserDto> UpdateUserAsync(IndividualUserDto updated)
        {
            if (updated is null)
                throw new ArgumentNullException(nameof(updated));
            
            AssertModelIsCorrect(updated);

            var mapped = MapViewModelToModel(updated);
            try
            {
                await _repository.UpdateUser(mapped);
            }
            catch (ItemNotFoundException)
            {
                // TODO
            }

            return updated;
        }

        private void AssertModelIsCorrect(IndividualUserDto individual)
        {
            if (String.IsNullOrEmpty(individual.FirstName) ||
                String.IsNullOrEmpty(individual.LastName))
                throw new ArgumentException("Wrong parameters");
        }
    }
}