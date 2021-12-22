﻿using System;
using System.Collections.Generic;
using AuctionDAL.Enums;
using AuctionDAL.Models;

namespace AuctionBLL.Dto
{
    public class LotDto
    {
        public Guid Id { get; set; }
        
        public LotStatus Status { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public MoneyDto StartPrice { get; set; }
        public MoneyDto ActualPrice { get; set; }
        public MoneyDto MinStepPrice { get; set; }
        
        public DateTime DateOfCreation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime ProlongationTime { get; set; }
        public DateTime TimeForStep { get; set; }
        
        public UserDto Owner { get; set; }
        public List<UserDto> Participants { get; set; }
        public List<LotStepLogDto> Steps { get; set; }
    }
}