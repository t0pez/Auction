using System.ComponentModel.DataAnnotations;

namespace AuctionWeb.ViewModels.Users
{
    public class LoginInfoModel
    {
        [Required]
        [MinLength(4)] 
        public string Login { get; set; }
        [Required]
        [MinLength(6)] 
        public string Password { get; set; }
    }
}