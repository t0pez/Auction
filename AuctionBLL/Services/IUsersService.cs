using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AuctionBLL.Dto;

namespace AuctionBLL.Services
{
    public interface IUsersService : IDisposable
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetByUserIdAsync(string userId);
        Task<IEnumerable<string>> GetUserRolesAsync(string userId);
        Task CreateAsync(UserDto newUser);
        Task CreateUserMoneyAsync(string userId, MoneyDto money);
        Task<ClaimsIdentity> LoginAsync(UserDto user);
        Task AddRoleAsync(string userId, string role);
        Task RemoveRoleAsync(string userId, string role);
        Task UpdateAsync(UserDto updated);
    }
}
