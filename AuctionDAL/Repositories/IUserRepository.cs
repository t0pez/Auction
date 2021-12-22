using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionDAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuctionDAL.Repositories
{
    public interface IUserRepository<TUser> : IUserStore<TUser> where TUser : User
    {
        Task<IEnumerable<TUser>> GetAllUsersAsync();
        // Task<TUser> GetUserByIdAsync(Guid id);
        // Task CreateUserAsync(TUser user);
        // Task UpdateUser(TUser updated);
    }
}
