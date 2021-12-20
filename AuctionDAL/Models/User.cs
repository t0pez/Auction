using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuctionDAL.Models
{
    public abstract class User : IdentityUser
    {
        public virtual Wallet Wallet { get; set; }
        public virtual ICollection<Lot> OwnedLots { get; set; }
        public virtual ICollection<Lot> LotsAsParticipant { get; set; }
    }
}
