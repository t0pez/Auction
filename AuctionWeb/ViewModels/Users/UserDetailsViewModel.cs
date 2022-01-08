using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AuctionBLL.Dto;

namespace AuctionWeb.ViewModels.Users
{
    public class UserDetailsViewModel
    {
        public string Id { get; set; }
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Roles")]
        public string Role { get; set; }

        [Display(Name = "Wallet")]
        public WalletDto Wallet { get; set; }
        
        [Display(Name = "Owned lots")]
        public IEnumerable<LotDto> OwnedLots { get; set; } = new List<LotDto>();
        
        [Display(Name = "Lots as participants")]
        public IEnumerable<LotDto> AsParticipant { get; set; } = new List<LotDto>();
        
        [Display(Name = "Acquired lots")]
        public IEnumerable<LotDto> AcquiredLots { get; set; } = new List<LotDto>();
    }
}