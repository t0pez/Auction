using System;
using System.ComponentModel.DataAnnotations;

namespace AuctionWeb.ViewModels.Money
{
    public class MoneyTopUpViewModel
    {
        [Required] 
        public Guid MoneyId { get; set; }
        [Required]
        [Display(Name = "Amount to add")]
        public decimal AddedAmount { get; set; }
    }
}