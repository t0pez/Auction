using AuctionDAL.Models.Base;

namespace AuctionDAL.Models
{
    public class Money : BaseModel
    {
        public decimal Amount { get; set; }
        public int Currency { get; set; }

        protected Money()
        {
            
        }
    }
}
