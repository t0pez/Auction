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
    public class IndividualUsersService : IIndividualUserService
    {
        private readonly IUserRepository<IndividualUser> _repository;
        private readonly IMapper _mapper;

        public IndividualUsersService(IUserRepository<IndividualUser> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private IEnumerable<IndividualUserViewModel> MapModelsToViewModels(IEnumerable<IndividualUser> unmapped) =>
            _mapper.Map<IEnumerable<IndividualUser>, IEnumerable<IndividualUserViewModel>>(unmapped);

        private IndividualUserViewModel MapModelToViewModel(IndividualUser unmapped) =>
            _mapper.Map<IndividualUser, IndividualUserViewModel>(unmapped);
        
        private IndividualUser MapViewModelToModel(IndividualUserViewModel unmapped) =>
            _mapper.Map<IndividualUserViewModel, IndividualUser>(unmapped);

        
        public async Task<IEnumerable<IndividualUserViewModel>> GetAllUsersAsync()
        {
            var unmapped = await _repository.GetAllUsersAsync();

            var mapped = MapModelsToViewModels(unmapped);

            return mapped;
        }

        public async Task<IndividualUserViewModel> GetUserByIdAsync(Guid id)
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
                return new IndividualUserViewModel();
            }
        }

        public async Task<IndividualUserViewModel> CreateUserAsync(IndividualUserViewModel newUser)
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

        public async Task<IndividualUserViewModel> UpdateUserAsync(IndividualUserViewModel updated)
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

        private void AssertModelIsCorrect(IndividualUserViewModel individual)
        {
            if (String.IsNullOrEmpty(individual.FirstName) ||
                String.IsNullOrEmpty(individual.LastName))
                throw new ArgumentException("Wrong parameters");
        }
    }
}