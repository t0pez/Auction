using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionDAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuctionDAL.Repositories
{
    public interface IUsersRepository
    {
        UserManager<User> UserManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }

        Task<int> SaveChangesAsync();
    }
}
