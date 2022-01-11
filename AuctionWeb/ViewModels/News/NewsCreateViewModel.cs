using System.ComponentModel.DataAnnotations;

namespace AuctionWeb.ViewModels.News
{
    public class NewsCreateViewModel
    {
        [Required] public string Title { get; set; }
        [Required] public string Text { get; set; }
    }
}