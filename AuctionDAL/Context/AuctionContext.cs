using System.Data.Entity;
using AuctionDAL.Models;

namespace AuctionDAL.Context
{
    public class AuctionContext : DbContext
    {
        // TODO: Add database file (use Visual Studio templates)
        // TODO: Edit config files
        // TODO: DbSets for logs (one for all or split?)
        
        public DbSet<IndividualUser> IndividualUsers { get; set; }
        public DbSet<EntityUser> EntityUsers { get; set; }
        public DbSet<Lot> Lots { get; set; } 
    }
}