using System;
using System.ComponentModel.DataAnnotations;
using AuctionBLL.Dto;

namespace AuctionWeb.ViewModels.Lots
{
    public class CreateModel
    {
        [Required] [MinLength(6)] public string Name { get; set; }
        [Required] [MinLength(10)] public string Description { get; set; }

        [Required] public MoneyDto StartPrice { get; set; }
        [Required] public MoneyDto MinStep { get; set; }


        [Required] public DateTime StartTime { get; set; }
        [Required] public TimeSpan ProlongationTime { get; set; }
        [Required] public TimeSpan TimeForStep { get; set; }
    }
}