using System.ComponentModel.DataAnnotations;

namespace AuctionWeb.ViewModels.Users
{
    public class MoneyCreateViewModel
    {
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string Currency { get; set; }
    }
}