using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuctionBLL.Dto;
using AuctionDAL;
using AuctionDAL.Models;
using AuctionDAL.Repositories;
using AutoMapper;
using Microsoft.AspNet.Identity;

namespace AuctionBLL.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = null;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var unmapped = await _unitOfWork.UserManager.Users.ToListAsync();

            var mapped = _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(unmapped);

            return mapped;
        }
        
        public async Task CreateAsync(UserDto newUser)
        {
            var user = await _unitOfWork.UserManager.FindByNameAsync(newUser.UserName);

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

            var operationResult = await _unitOfWork.UserManager.CreateAsync(user, newUser.Password);

            if (operationResult.Errors.Any())
                throw new InvalidOperationException("Smth has go wrong");

            await _unitOfWork.UserManager.AddToRoleAsync(user.Id, "user");
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ClaimsIdentity> LoginAsync(UserDto userDto)
        {
            var user = await _unitOfWork.UserManager.FindAsync(userDto.UserName, userDto.Password);

            if (user is null)
                throw new InvalidOperationException("User not found");

            var result = await _unitOfWork.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

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