using System.Threading.Tasks;
using AuctionDAL.Interfaces;
using AuctionDAL.Models;
using AuctionDAL.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuctionDAL
{
    public interface IUnitOfWork
    {
        ILotsRepository LotsRepository { get; }
        INewsRepository NewsRepository { get; }
        UserManager<User> UserManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }

        Task<int> SaveChangesAsync();
    }
}