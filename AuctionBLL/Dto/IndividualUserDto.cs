using System;
using System.Collections.Generic;
using AuctionDAL.Models;

namespace AuctionBLL.Dto
{
    public class IndividualUserDto : UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
