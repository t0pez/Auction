using System;
using System.Collections.Generic;
using AuctionDAL.Models;

namespace AuctionBLL.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Role { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public WalletDto Wallet { get; set; }

        public ICollection<LotDto> OwnedLots { get; set; }
        public ICollection<LotDto> AsParticipant { get; set; }
    }
}
