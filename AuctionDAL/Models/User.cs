using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuctionDAL.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public virtual Wallet Wallet { get; set; }
        public virtual ICollection<Lot> OwnedLots { get; set; }
        public virtual ICollection<Lot> LotsAsParticipant { get; set; }
        public virtual ICollection<Lot> AcquiredLots { get; set; }
    }
}
