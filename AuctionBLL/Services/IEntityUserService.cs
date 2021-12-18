using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionBLL.ViewModels;
using AuctionDAL.Models;

namespace AuctionBLL.Services
{
    public interface IEntityUserService
    {
        Task<IEnumerable<EntityUserViewModel>> GetAllUsersAsync();
        Task<EntityUserViewModel> GetUserByIdAsync(Guid id);
        Task<EntityUserViewModel> CreateUserAsync(EntityUserViewModel newUser);
        Task<EntityUserViewModel> UpdateUserAsync(EntityUser updated);
    }
}