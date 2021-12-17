using System;
using System.Collections.Generic;
using AuctionDAL.Models;

namespace AuctionBLL.ViewModels
{
    public class LotViewModel
    {
        public Guid Id { get; set; }
        
        public bool IsOpen { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public User Owner { get; set; }
        public List<User> Participants { get; set; }
        
        public MoneyViewModel StartPrice { get; set; }
        public MoneyViewModel ActualPrice { get; set; }

        public DateTime DateOfCreation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
