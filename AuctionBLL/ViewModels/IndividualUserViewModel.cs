using System;
using System.Collections.Generic;
using AuctionDAL.Models;

namespace AuctionBLL.ViewModels
{
    public class IndividualUserViewModel
    {
        public Guid Id { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Wallet Type { get; set; }

        public ICollection<LotViewModel> OwnedLots { get; set; }
        public ICollection<LotViewModel> AsParticipant { get; set; }
    }
}
