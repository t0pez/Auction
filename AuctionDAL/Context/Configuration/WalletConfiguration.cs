using System.Data.Entity.ModelConfiguration;
using AuctionDAL.Models;

namespace AuctionDAL.Context.Configuration
{
    public class WalletConfiguration : EntityTypeConfiguration<Wallet>
    {
        public WalletConfiguration()
        {
            HasKey(wallet => wallet.Id);

            HasMany(wallet => wallet.Money);
        }
    }
}