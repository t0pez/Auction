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
        public decimal StartPrice { get; set; }
        [Required] 
        public decimal MinStepPrice { get; set; }

        [Required] 
        [DataType(DataType.Date)] 
        public DateTime StartDate { get; set; }
        [Required] 
        [DataType(DataType.Time)] 
        public DateTime StartTime { get; set; }

        [Required] 
        [DataType(DataType.Time)] 
        public TimeSpan ProlongationTime{ get; set; }
        [Required] 
        [DataType(DataType.Time)] 
        public TimeSpan TimeForStep { get; set; }
    }
}