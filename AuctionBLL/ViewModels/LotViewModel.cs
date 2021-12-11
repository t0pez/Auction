using System;
using System.Collections.Generic;
using System.Linq;
using AuctionDAL;
using AuctionDAL.Models;

namespace AuctionBLL.ViewModels
{
    public class LotViewModel
    {
        public bool IsOpen { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public User Owner { get; set; }
        public IEnumerable<User> Participants { get; set; }
        
        public Money StartPrice { get; set; }
        public Money ActualPrice { get; set; }

        public DateTime DateOfCreation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int ParticipantsCount => Participants.Count();
    }
}
