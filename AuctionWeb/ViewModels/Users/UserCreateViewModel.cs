using System.ComponentModel.DataAnnotations;

namespace AuctionWeb.ViewModels.Users
{
    public class UserCreateViewModel
    {
        [Required] [MinLength(4)] public string UserName { get; set; }

        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required] [MinLength(5)] public string FirstName { get; set; }
        [Required] [MinLength(5)] public string LastName { get; set; }
    }
}