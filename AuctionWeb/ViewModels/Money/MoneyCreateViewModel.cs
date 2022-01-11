using System.ComponentModel.DataAnnotations;

namespace AuctionWeb.ViewModels.Money
{
    public class MoneyCreateViewModel
    {
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string Currency { get; set; }
    }
}