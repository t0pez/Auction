using AuctionDAL.Logs;

namespace AuctionDAL.Models
{
    public class LotLog : Log
    {
        public virtual Lot Lot { get; set; }
    }
}