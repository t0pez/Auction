using System;

namespace AuctionBLL.Dto
{
    public class NewsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}