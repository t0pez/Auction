using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionDAL.Models;

namespace AuctionDAL.Repositories
{
    public interface ILotsRepository
    {
        Task<IEnumerable<Lot>> GetAllLotsAsync();
        Task<IEnumerable<Lot>> GetLotsByPredicateAsync(Func<Lot, bool> predicate);
        Task<Lot> GetLotByIdAsync(Guid id);
        Task CreateLotAsync(Lot lot);
        Task UpdateLotAsync(Lot updated);
    }
}
