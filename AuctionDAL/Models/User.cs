using System.Collections.Generic;

namespace AuctionDAL.Models
{
    public class User : BaseModel
    {
        public Wallet Wallet { get; set; }

        public virtual ICollection<Lot> OwnedLots { get; set; }
        public virtual ICollection<Lot> Lots { get; set; }
    }
}
