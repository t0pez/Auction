using System;
using System.Collections.Generic;
using AuctionDAL.Models.Base;

namespace AuctionDAL.Models
{
    public class Wallet : BaseModel
    {
        public virtual ICollection<Money> Money { get; set; }

        public Wallet(Guid id)
        {
            Id = id;
        }
        
        private Wallet()
        {
        }
    }
}
