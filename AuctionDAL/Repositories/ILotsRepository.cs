using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionDAL.Models;

namespace AuctionDAL.Repositories
{
    public interface ILotsRepository
    {
        Task<IEnumerable<Lot>> GetAllLotsAsync();
        Task<IEnumerable<Lot>> GetAllCreatedLotsAsync();
        Task<IEnumerable<Lot>> GetAllOpenedLotsAsync();
        Task<IEnumerable<Lot>> GetAllClosedLotsAsync();
        Task<Lot> GetLotByIdAsync(Guid id);
        Task CreateLotAsync(Lot lot);
        Task UpdateLotAsync(Lot updated);
    }
}
