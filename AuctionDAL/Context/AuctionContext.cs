using System.Data.Entity;
using AuctionDAL.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuctionDAL.Context
{
    public class AuctionContext : IdentityDbContext<User>
    {
        public AuctionContext() : base("AuctionContext")
        {
        }
        
        public DbSet<Lot> Lots { get; set; }
    }
}
