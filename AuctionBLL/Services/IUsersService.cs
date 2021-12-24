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
        Task CreateAsync(UserDto newUser);
        Task<ClaimsIdentity> LoginAsync(UserDto user);
        Task UpdateAsync(UserDto updated);
    }
}
