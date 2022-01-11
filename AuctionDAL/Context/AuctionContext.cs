using System.Data.Entity;
using AuctionDAL.Context.Configuration;
using AuctionDAL.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuctionDAL.Context
{
    public class AuctionContext : IdentityDbContext<User>
    {
        public AuctionContext() : base("AuctionContext")
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
        }
        
        public DbSet<Lot> Lots { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Money> Money { get; set; }
        public DbSet<News> News { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new LotConfiguration());
            modelBuilder.Configurations.Add(new WalletConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new NewsConfiguration());
        }
    }
}
