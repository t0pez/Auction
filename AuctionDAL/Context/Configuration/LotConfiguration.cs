using System.Data.Entity.ModelConfiguration;
using AuctionDAL.Models;

namespace AuctionDAL.Context.Configuration
{
    public class LotConfiguration : EntityTypeConfiguration<Lot>
    {
        public LotConfiguration()
        {
            HasKey(lot => lot.Id);

            Property(lot => lot.Name).IsRequired();
            Property(lot => lot.Description).IsRequired();
            Property(lot => lot.Status).IsRequired();
            Property(lot => lot.DateOfCreation).IsRequired();
            Property(lot => lot.ProlongationTime).IsRequired();
            Property(lot => lot.TimeForStep).IsRequired();

            HasRequired(lot => lot.Owner)
                .WithMany(user => user.OwnedLots)
                .HasForeignKey(lot => lot.OwnerId);

            //HasOptional(lot => lot.Acquirer)
            //    .WithMany(user => user.AcquiredLots)
            //    .HasForeignKey(lot => lot.AcquirerId);

            HasMany(lot => lot.Participants)
                .WithMany(user => user.LotsAsParticipant);
        }
    }
}