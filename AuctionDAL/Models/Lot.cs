﻿using System;
using System.Collections.Generic;
using AuctionDAL.Enums;
using AuctionDAL.Logs;
using AuctionDAL.Models.Base;

namespace AuctionDAL.Models
{
    public class Lot : BaseModel
    {
        public LotStatus Status { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public Money StartPrice { get; set; }
        public Money HighestPrice { get; set; }
        public Money MinStepPrice { get; set; }
        
        public DateTime DateOfCreation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan ProlongationTime { get; set; }
        public TimeSpan TimeForStep { get; set; }
        
        public virtual User Owner { get; set; }
        public virtual User? Buyer { get; set; }
        public virtual IEnumerable<User> Participants { get; set; }
        public virtual IEnumerable<LotStepLog> Steps { get; set; }
    }
}
