using System.Collections.Generic;

namespace AuctionBLL.Dto
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Role { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public WalletDto Wallet { get; set; } = new WalletDto();

        public ICollection<LotDto> OwnedLots { get; set; } = new List<LotDto>();
        public ICollection<LotDto> AsParticipant { get; set; } = new List<LotDto>();
        public ICollection<LotDto> AcquiredLots { get; set; } = new List<LotDto>();
    }
}
