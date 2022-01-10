using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuctionBLL.Dto;
using AuctionBLL.Interfaces;
using AuctionDAL;
using AuctionDAL.Extensions.Models;
using AuctionDAL.Models;
using AutoMapper;
using Microsoft.AspNet.Identity;

namespace AuctionBLL.Services
{
    /// <inheritdoc cref="IUsersService"/>
    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsersService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var unmapped = await _unitOfWork.UserManager.Users.ToListAsync();

            var mapped = _mapper.Map<IEnumerable<UserDto>>(unmapped);

            return mapped;
        }

        public async Task<UserDto> GetByUserIdAsync(string userId)
        {
            var unmapped = await _unitOfWork.UserManager.Users.FirstOrDefaultAsync(user => user.Id == userId);

            if (unmapped is null)
                throw new InvalidOperationException("User not found");

            var mapped = _mapper.Map<UserDto>(unmapped);

            return mapped;
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(string userId)
        {
            return await _unitOfWork.UserManager.GetRolesAsync(userId);
        }

        public async Task CreateAsync(UserDto newUser)
        {
            var user = await _unitOfWork.UserManager.FindByNameAsync(newUser.UserName);

            if (user is not null)
                throw new InvalidOperationException("User already exists");

            user = _mapper.Map<User>(newUser);

            user.Id = Guid.NewGuid().ToString();
            user.Wallet = new Wallet {Id = Guid.NewGuid()};

            var operationResult = await _unitOfWork.UserManager.CreateAsync(user, newUser.Password);

            if (operationResult.Errors.Any())
                throw new InvalidOperationException("Smth has go wrong");

            await _unitOfWork.UserManager.AddToRoleAsync(user.Id, newUser.Role);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CreateUserMoneyAsync(string userId, MoneyDto money)
        {
            var user = await _unitOfWork.UserManager.FindByIdAsync(userId);

            if (user is null)
                throw new InvalidOperationException("User not found");
            if(user.HasMoneyOfCurrency(money.Currency.Value))
                throw new ArgumentException("Already has account of this currency");

            var mappedMoney = _mapper.Map<Money>(money); 
            
            user.Wallet.Money.Add(mappedMoney);
            await _unitOfWork.UserManager.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ClaimsIdentity> LoginAsync(UserDto userDto)
        {
            var user = await _unitOfWork.UserManager.FindAsync(userDto.UserName, userDto.Password);

            if (user is null)
                throw new InvalidOperationException("Wrong login or password");

            var result = await _unitOfWork.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            return result;
        }

        public async Task AddRoleAsync(string userId, string role)
        {
            var user = await _unitOfWork.UserManager.FindByIdAsync(userId);

            if (user is null)
                throw new InvalidOperationException("User not found");

            await _unitOfWork.UserManager.AddToRoleAsync(userId, role);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RemoveRoleAsync(string userId, string role)
        {
            var user = await _unitOfWork.UserManager.FindByIdAsync(userId);

            if (user is null)
                throw new InvalidOperationException("User not found");

            await _unitOfWork.UserManager.RemoveFromRoleAsync(userId, role);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<UserDto> TopUpUserBalanceAsync(string userId, Guid moneyId, decimal addedAmount)
        {
            if (addedAmount < 0)
                throw new ArgumentException("Added amount can not be less than zero");

            var user = await _unitOfWork.UserManager.FindByIdAsync(userId);

            if (user is null)
                throw new InvalidOperationException("User not found");

            var userMoneyAmount = user.GetMoneyById(moneyId);
            userMoneyAmount.Amount += addedAmount;

            await _unitOfWork.UserManager.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            var mapped = _mapper.Map<UserDto>(user);
            return mapped;
        }
        
        public void Dispose()
        {
            
        }
    }
}