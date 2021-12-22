using System.Data.Entity;
using AuctionDAL.Models;

namespace AuctionDAL.Context
{
    public class AuctionContext : DbContext
    {
        public AuctionContext() : base("AuctionContext")
        {
        }
        
        public DbSet<Lot> Lots { get; set; }
        public DbSet<IndividualUser> Individuals { get; set; }
        public DbSet<EntityUser> Entities { get; set; }
    }
}
