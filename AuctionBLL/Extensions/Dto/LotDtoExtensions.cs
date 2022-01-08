using System;
using AuctionBLL.Dto;
using AuctionBLL.Enums;

namespace AuctionBLL.Extensions.Dto
{
    public static class LotDtoExtensions
    {
        public static void CreatingInitialize(this LotDto lotDto)
        {
            lotDto.Id = Guid.NewGuid();
            lotDto.Status = LotStatus.Created;
            lotDto.DateOfCreation = DateTime.Now;
            lotDto.ActualPrice = lotDto.StartPrice;
        }
    }
}