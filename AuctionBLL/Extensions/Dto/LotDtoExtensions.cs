using System;
using AuctionBLL.Dto;
using AuctionBLL.Enums;

namespace AuctionBLL.Extensions.Dto
{
    public static class LotDtoExtensions
    {
        /// <summary>
        /// Initializes start data
        /// </summary>
        /// <remarks>
        /// Sets new Id, Status, Date and Actual Price
        /// </remarks>
        /// <param name="lotDto"></param>
        public static void CreatingInitialize(this LotDto lotDto)
        {
            lotDto.Id = Guid.NewGuid();
            lotDto.Status = LotStatus.Created;
            lotDto.DateOfCreation = DateTime.Now;
            lotDto.ActualPrice = new MoneyDto(lotDto.StartPrice.Amount, lotDto.StartPrice.Currency);
        }
    }
}