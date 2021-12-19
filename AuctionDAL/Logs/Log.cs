using System;
using AuctionDAL.Models.Base;

namespace AuctionDAL.Logs
{
    public abstract class Log : BaseModel
    {
        public DateTime DateOfCreation { get; set; }
    }
}