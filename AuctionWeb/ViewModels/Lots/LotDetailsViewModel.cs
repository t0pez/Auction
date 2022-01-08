using System;
using System.Collections.Generic;
using AuctionBLL.Dto;
using AuctionBLL.Enums;

namespace AuctionWeb.ViewModels.Lots
{
    public class LotDetailsViewModel
    {
        public Guid Id { get; set; }
        public LotStatus Status { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public MoneyDto ActualPrice { get; set; }
        public MoneyDto StartPrice { get; set; }
        public MoneyDto MinStepPrice { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public TimeSpan ProlongationTime { get; set; }
        public TimeSpan TimeForStep { get; set; }

        public UserDto Owner { get; set; }
        public IEnumerable<UserDto> Participants { get; set; }
    }
}