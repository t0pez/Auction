using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionBLL.Dto;
using AuctionDAL.Models;

namespace AuctionBLL.Services
{
    public interface ILotsService
    {
        Task<IEnumerable<LotDto>> GetAllLotsAsync();
        Task<IEnumerable<LotDto>> GetAllCreatedLotsAsync();
        Task<IEnumerable<LotDto>> GetAllOpenedLotsAsync();
        Task<IEnumerable<LotDto>> GetAllClosedLotsAsync();
        Task<LotDto> GetLotByIdAsync(Guid id);
        Task<LotDto> CreateLotAsync(LotDto lot);
        Task<LotDto> AddParticipantAsync(LotDto lot, UserDto user);
        Task<LotDto> SetLotActualPriceAsync(LotDto lot, UserDto user, MoneyDto newPrice);
        Task<LotDto> OpenLotAsync(LotDto lot);
        Task<LotDto> CloseLotAsync(LotDto dto);
    }
}
