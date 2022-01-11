using System.Collections.Generic;
using AuctionDAL.Models.Base;

namespace AuctionDAL.Models
{
    public class Wallet : BaseModel
    {
        public virtual ICollection<Money> Money { get; set; }
    }
}
