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
    public class EntityUsersService : IEntityUsersService
    {
        private readonly IUserRepository<EntityUser> _repository;
        private readonly IMapper _mapper;

        public EntityUsersService(IUserRepository<EntityUser> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        private IEnumerable<EntityUserDto> MapModelsToDto(IEnumerable<EntityUser> unmapped) =>
            _mapper.Map<IEnumerable<EntityUser>, IEnumerable<EntityUserDto>>(unmapped);
        private EntityUserDto MapModelToDto(EntityUser unmapped) => _mapper.Map<EntityUser, EntityUserDto>(unmapped);
        private EntityUser MapViewModelToDto(EntityUserDto unmapped) => _mapper.Map<EntityUserDto, EntityUser>(unmapped);

        public async Task<IEnumerable<EntityUserDto>> GetAllUsersAsync()
        {
            var unmapped = await _repository.GetAllUsersAsync();

            var mapped = MapModelsToDto(unmapped);

            return mapped;
        }
        
        public async Task<EntityUserDto> GetUserByIdAsync(Guid id)
        {
            try
            {
                var unmapped = await _repository.GetUserByIdAsync(id);

                var mapped = MapModelToDto(unmapped);

                return mapped;
            }
            catch (ItemNotFoundException)
            {
                throw;
            }
        }

        public async Task<EntityUserDto> CreateUserAsync(EntityUserDto newUser)
        {
            if (newUser is null)
                throw new ArgumentNullException(nameof(newUser));
            
            AssertModelIsCorrect(newUser);

            var mapped = MapViewModelToDto(newUser);
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

        public async Task<EntityUserDto> UpdateUserAsync(EntityUserDto updated)
        {
            if (updated is null)
                throw new ArgumentNullException(nameof(updated));
            
            AssertModelIsCorrect(updated);

            var mapped = MapViewModelToDto(updated);
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

        private void AssertModelIsCorrect(EntityUserDto dto)
        {
            if (String.IsNullOrEmpty(dto.Name))
                throw new ArgumentException("Wrong parameters");
        }
    }
}