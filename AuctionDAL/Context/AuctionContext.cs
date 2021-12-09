using System.Data.Entity;
using AuctionDAL.Models;

namespace AuctionDAL.Context
{
    public class AuctionContext : DbContext
    {
        public DbSet<Lot> Lots { get; set; } 
    }
}