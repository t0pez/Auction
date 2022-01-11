using System.Data.Entity.ModelConfiguration;
using AuctionDAL.Models;

namespace AuctionDAL.Context.Configuration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasKey(user => user.Id);

            Property(user => user.FirstName).IsRequired();
            Property(user => user.LastName).IsRequired();

            HasMany(user => user.OwnedLots)
                .WithRequired(lot => lot.Owner)
                .HasForeignKey(lot => lot.OwnerId)
                .WillCascadeOnDelete(false);

            HasMany(user => user.LotsAsParticipant)
                .WithMany(lot => lot.Participants)
                .Map(configuration =>
                    configuration.MapLeftKey("UserRefId").MapRightKey("LotRefId").ToTable("LotParticipant"));

            HasMany(user => user.AcquiredLots)
                .WithRequired(lot => lot.Acquirer)
                .HasForeignKey(lot => lot.AcquirerId)
                .WillCascadeOnDelete(false);

        }
    }
}