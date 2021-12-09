using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionDAL.Models;

namespace AuctionDAL.Repositories
{
    public interface ILotsRepository
    {
        Task<IEnumerable<Lot>> GetAllLotsAsync();
        Task<IEnumerable<Lot>> GetAllOpenLotsAsync();
        Task<IEnumerable<Lot>> GetAllClosedLotsAsync();
        Task<Lot> GetLotByIdAsync(Guid id);
        Task<bool> UpdateLotAsync(Lot updated);
        Task<bool> DeleteAsync(Guid id);
    }
}