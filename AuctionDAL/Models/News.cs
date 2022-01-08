using System;
using AuctionDAL.Models.Base;

namespace AuctionDAL.Models
{
    public class News : BaseModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}