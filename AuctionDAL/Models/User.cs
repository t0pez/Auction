using System.Collections.Generic;
using AuctionDAL.Models.Base;

namespace AuctionDAL.Models
{
    public abstract class User : BaseModel
    {
        public Wallet Wallet { get; set; }

        public virtual ICollection<Lot> OwnedLots { get; set; }
        public virtual ICollection<Lot> LotsAsParticipant { get; set; }
    }
}
