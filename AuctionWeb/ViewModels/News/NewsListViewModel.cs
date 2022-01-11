using System;

namespace AuctionWeb.ViewModels.News
{
    public class NewsListViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}