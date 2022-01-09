using System;
using System.ComponentModel.DataAnnotations;

namespace AuctionWeb.ViewModels.News
{
    public class NewsEditViewModel
    {
        [Required] public Guid Id { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Text { get; set; }
        [Required] public DateTime DateOfCreation { get; set; }
    }
}