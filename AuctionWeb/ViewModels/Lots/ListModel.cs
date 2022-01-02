using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AuctionBLL.Dto;
using AuctionBLL.Enums;

namespace AuctionWeb.ViewModels.Lots
{
    public class ListModel
    {
        public Guid Id { get; set; }
        public LotStatus Status { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public MoneyDto ActualPrice { get; set; }

        // TODO: Set start price?
    }
}