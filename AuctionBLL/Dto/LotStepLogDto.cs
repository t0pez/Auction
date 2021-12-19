using System;

namespace AuctionBLL.Dto
{
    public class LotStepLogDto
    {
        public Guid Id { get; set; }
        public DateTime DateOfCreation { get; set; }
        public MoneyDto Step { get; set; }
        public UserDto User { get; set; }
        public bool LotWasProlonged { get; set; }
    }
}