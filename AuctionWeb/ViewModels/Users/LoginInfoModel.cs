using System.ComponentModel.DataAnnotations;

namespace AuctionWeb.ViewModels.Users
{
    public class LoginInfoModel
    {
        [Required] public string Login { get; set; }
        [Required] public string Password { get; set; }
    }
}