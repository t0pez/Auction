using AuctionDAL.Models;

namespace AuctionDAL.Logs
{
    public class LotLog : Log
    {
        public virtual Lot Lot { get; set; }
    }
}