using System.Threading.Tasks;
using AuctionDAL.Models;
using AuctionDAL.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuctionDAL
{
    public interface IUnitOfWork
    {
        public ILotsRepository LotsRepository { get; }
        UserManager<User> UserManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }

        Task<int> SaveChangesAsync();
    }
}