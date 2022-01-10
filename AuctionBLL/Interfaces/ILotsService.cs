using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionBLL.Dto;

namespace AuctionBLL.Interfaces
{
    /// <summary>
    /// Service for work with lot
    /// </summary>
    public interface ILotsService
    {
        /// <summary>
        /// Gets all the lots
        /// </summary>
        /// <returns>Mapped lots collection</returns>
        Task<IEnumerable<LotDto>> GetAllLotsAsync();
        /// <summary>
        /// Gets lot by id
        /// </summary>
        /// <param name="id">Lot id</param>
        /// <returns>Mapped lot</returns>
        /// <exception cref="InvalidOperationException">When lot not found</exception>
        Task<LotDto> GetLotByIdAsync(Guid id);
        /// <summary>
        /// Creates lot
        /// </summary>
        /// <param name="lotDto">Lot create model</param>
        /// <param name="ownerId">Lot owner</param>
        /// <returns>Mapped lot</returns>
        /// <exception cref="ArgumentNullException">When lotDto is null</exception>
        /// <exception cref="ArgumentException">When arguments is not correct</exception>
        /// <exception cref="InvalidOperationException">When user state is not correct or not found</exception>
        Task<LotDto> CreateLotAsync(LotDto lotDto, string ownerId);
        /// <summary>
        /// Adds a participant for a lot
        /// </summary>
        /// <param name="lotId">Lot id</param>
        /// <param name="userId">User id</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">When lot state is not correct or not found</exception>
        /// <exception cref="ArgumentException">When parameters state is not correct</exception>
        Task<LotDto> AddParticipantAsync(Guid lotId, string userId);
        /// <summary>
        /// Sets lot actual price
        /// </summary>
        /// <remarks>
        /// User sets as lots acquirer till next step or closing lot
        /// </remarks>
        /// <param name="lotId">Lot id</param>
        /// <param name="userId">User id</param>
        /// <param name="newPrice">New actual price</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">When lot or user state is not correct or not found</exception>
        /// <exception cref="ArgumentException">When parameters state is not correct</exception>
        Task<LotDto> SetLotActualPriceAsync(Guid lotId, string userId, decimal newPrice);
    }
}
