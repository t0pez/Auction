using AuctionDAL.Models;

namespace AuctionDAL.Logs
{
    public class LotStepLog : LotLog
    {
        public Money Step { get; set; }
        public bool LotWasProlonged { get; set; }
        public virtual User User { get; set; }
    }
}