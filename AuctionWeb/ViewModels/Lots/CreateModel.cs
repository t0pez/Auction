using System;
using System.ComponentModel.DataAnnotations;

namespace AuctionWeb.ViewModels.Lots
{
    public class CreateModel
    {
        [Required] 
        [MinLength(5)] 
        public string Name { get; set; }
        [Required] 
        [MinLength(20)] 
        public string Description { get; set; }


        [Required] 
        public string Currency { get; set; }
        [Required] 
        [Display(Name = "Start price")]
        public decimal StartPrice { get; set; }
        [Required]
        [Display(Name = "Minimal next step cost")]
        public decimal MinStepPrice { get; set; }

        [Required] 
        [DataType(DataType.Date)]
        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }
        [Required] 
        [DataType(DataType.Time)]
        [Display(Name = "Start time")]
        public TimeSpan StartTime { get; set; }

        [Required] 
        [DataType(DataType.Time)]
        [Display(Name = "Prolongation time")]
        public TimeSpan ProlongationTime{ get; set; }
        [Required] 
        [DataType(DataType.Time)]
        [Display(Name = "Time for step")]
        public TimeSpan TimeForStep { get; set; }
    }
}