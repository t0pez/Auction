using System.ComponentModel.DataAnnotations;

namespace AuctionWeb.ViewModels.News
{
    public class NewsCreateViewModel
    {
        [Required] [MinLength(5)] public string Title { get; set; }
        [Required] [MinLength(20)] public string Text { get; set; }
    }
}