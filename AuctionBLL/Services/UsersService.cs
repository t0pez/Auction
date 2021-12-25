using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuctionBLL.Dto;
using AuctionDAL.Models;
using AuctionDAL.Repositories;
using AutoMapper;
using Microsoft.AspNet.Identity;

namespace AuctionBLL.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _repository;
        private readonly IMapper _mapper;

        public UsersService(IUsersRepository repository)
        {
            _repository = repository;
            _mapper = null;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var unmapped = await _repository.UserManager.Users.ToListAsync();

            var mapped = _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(unmapped);

            return mapped;
        }
        
        public async Task CreateAsync(UserDto newUser)
        {
            var user = await _repository.UserManager.FindByNameAsync(newUser.UserName);

            if (user is not null)
                throw new InvalidOperationException("User already exists");

            user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = newUser.UserName,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Wallet = new Wallet{Id = Guid.NewGuid()},
                OwnedLots = new List<Lot>(),
                LotsAsParticipant = new List<Lot>()
            };

            var operationResult = await _repository.UserManager.CreateAsync(user, newUser.Password);

            if (operationResult.Errors.Any())
                throw new InvalidOperationException("Smth has go wrong");

            await _repository.UserManager.AddToRoleAsync(user.Id, "user");
            await _repository.SaveChangesAsync();
        }

        public async Task<ClaimsIdentity> LoginAsync(UserDto userDto)
        {
            var user = await _repository.UserManager.FindAsync(userDto.UserName, userDto.Password);

            if (user is null)
                throw new InvalidOperationException("User not found");

            var result = await _repository.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            return result;
        }

        public Task UpdateAsync(UserDto updated)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }
    }
}