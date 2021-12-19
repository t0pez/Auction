using System;
using System.Collections.Generic;
using AuctionDAL.Models;

namespace AuctionBLL.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        
        public Wallet Type { get; set; } // TODO: change to non-DAL entity

        public ICollection<LotDto> OwnedLots { get; set; }
        public ICollection<LotDto> AsParticipant { get; set; }
    }
}