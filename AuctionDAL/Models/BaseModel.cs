using System;
using System.ComponentModel.DataAnnotations;

namespace AuctionDAL.Models
{
    public class BaseModel
    {
        [Key]
        public Guid Id { get; set; }
    }
}