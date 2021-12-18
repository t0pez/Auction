using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionDAL.Models;

namespace AuctionDAL.Repositories
{
    public interface IUserRepository<TUser> where TUser : User
    {
        // TODO: another implementation
        Task<IEnumerable<TUser>> GetAllUsersAsync();
        Task<TUser> GetUserByIdAsync(Guid id);
        Task CreateUserAsync(TUser user);
        Task UpdateUser(TUser updated);
    }
}
