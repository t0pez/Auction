using System;
using System.Collections.Generic;

namespace AuctionBLL.Dto
{
    public class WalletDto
    {
        public Guid Id { get; set; }
        public ICollection<MoneyDto> Moneys { get; set; }
    }
}