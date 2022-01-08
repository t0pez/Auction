using System.Data.Entity.ModelConfiguration;
using AuctionDAL.Models;

namespace AuctionDAL.Context.Configuration
{
    public class NewsConfiguration : EntityTypeConfiguration<News>
    {
        public NewsConfiguration()
        {
            HasKey(news => news.Id);

            Property(news => news.Title).IsRequired();
            Property(news => news.Text).IsRequired();
            Property(news => news.DateOfCreation).IsRequired();
        }
    }
}