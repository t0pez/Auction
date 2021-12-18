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
    public class EntityUsersService : IEntityUserService
    {
        private readonly IUserRepository<EntityUser> _repository;
        private readonly IMapper _mapper;

        public EntityUsersService(IUserRepository<EntityUser> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private IEnumerable<EntityUserViewModel> MapEntitiesModelsToViewModels(IEnumerable<EntityUser> unmapped) => _mapper.Map<IEnumerable<EntityUser>, IEnumerable<EntityUserViewModel>>(unmapped);
        private EntityUserViewModel MapEntityToViewModel(EntityUser unmapped) => _mapper.Map<EntityUser, EntityUserViewModel>(unmapped);

        public async Task<IEnumerable<EntityUserViewModel>> GetAllUsersAsync()
        {
            var unmapped = await _repository.GetAllUsersAsync();

            var mapped = MapEntitiesModelsToViewModels(unmapped);

            return mapped;
        }
        
        public async Task<EntityUserViewModel> GetUserByIdAsync(Guid id)
        {
            try
            {
                var unmapped = await _repository.GetUserByIdAsync(id);

                var mapped = MapEntityToViewModel(unmapped);

                return mapped;
            }
            catch (ItemNotFoundException)
            {
                throw;
            }
        }

        public Task<EntityUserViewModel> CreateUserAsync(EntityUserViewModel newUser)
        {
            throw new NotImplementedException();
        }

        public Task<EntityUserViewModel> UpdateUserAsync(EntityUser updated)
        {
            throw new NotImplementedException();
        }

        private void AssertModelIsCorrect(EntityUser entity)
        {
            if (String.IsNullOrEmpty(entity.Name))
                throw new ArgumentException("Wrong parameters");
        }
    }
}