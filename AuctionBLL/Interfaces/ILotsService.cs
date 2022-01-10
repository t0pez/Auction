using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionBLL.Dto;

namespace AuctionBLL.Interfaces
{
    public interface ILotsService
    {
        Task<IEnumerable<LotDto>> GetAllLotsAsync();
        Task<LotDto> GetLotByIdAsync(Guid id);
        Task<LotDto> CreateLotAsync(LotDto lotDto, string ownerId);
        Task<LotDto> AddParticipantAsync(Guid lot, string user);
        Task<LotDto> SetLotActualPriceAsync(Guid lotId, string userId, decimal newPrice);
    }
}
