using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionBLL.Dto;

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
        Task<LotDto> AddParticipantAsync(Guid lot, string user);
        Task<LotDto> SetLotActualPriceAsync(Guid lotId, string userId, decimal newPrice);
        Task<LotDto> OpenLotAsync(Guid lotId);
        Task<LotDto> CloseLotAsync(LotDto dto);
    }
}
