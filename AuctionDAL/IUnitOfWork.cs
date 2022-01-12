using AuctionDAL.Interfaces;
using AuctionDAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

namespace AuctionDAL
{
    public interface IUnitOfWork //: IDisposable
    {
        ILotsRepository LotsRepository { get; }
        INewsRepository NewsRepository { get; }
        UserManager<User> UserManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }

        int SaveChanges();
    }
}