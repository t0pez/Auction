using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionBLL.Dto;
using AuctionDAL.Models;

namespace AuctionBLL.Services
{
    public interface IEntityUsersService
    {
        Task<IEnumerable<EntityUserDto>> GetAllUsersAsync();
        Task<EntityUserDto> GetUserByIdAsync(Guid id);
        Task<EntityUserDto> CreateUserAsync(EntityUserDto newUser);
        Task<EntityUserDto> UpdateUserAsync(EntityUserDto updated);
    }
}