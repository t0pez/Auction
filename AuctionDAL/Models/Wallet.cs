using System.Collections.Generic;
using AuctionDAL.Models.Base;

namespace AuctionDAL.Models
{
    public class Wallet : BaseModel
    {
        // TODO: public event NewAccountCreated 
        // TODO: public event MoneyBalanceChanged 
        public virtual ICollection<Money> Money { get; set; }
    }
}
