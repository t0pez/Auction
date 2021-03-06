using System;
using System.Collections.Generic;

namespace AuctionBLL.Dto
{
    public class WalletDto
    {
        public Guid Id { get; set; }
        public ICollection<MoneyDto> Money { get; set; } = new List<MoneyDto>();
    }
}