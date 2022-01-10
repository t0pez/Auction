using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionDAL.Exceptions;
using AuctionDAL.Models;

namespace AuctionDAL.Interfaces
{
    /// <summary>
    /// Repository for entity lots
    /// </summary>
    public interface ILotsRepository
    {
        /// <summary>
        /// Gets all lots
        /// </summary>
        /// <returns>Lots collection</returns>
        Task<IEnumerable<Lot>> GetAllLotsAsync();
        
        /// <summary>
        /// Gets lots by predicate
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns>Lots collection</returns>
        Task<IEnumerable<Lot>> GetLotsByPredicateAsync(Func<Lot, bool> predicate);
        
        /// <summary>
        /// Gets lot by id
        /// </summary>
        /// <param name="id">Lot id</param>
        /// <returns>Lot</returns>
        /// <exception cref="ItemNotFoundException">When lot not found</exception>
        Task<Lot> GetLotByIdAsync(Guid id);

        /// <summary>
        /// Creates lot
        /// </summary>
        /// <param name="lot">Lot</param>
        /// <returns></returns>
        /// <exception cref="ItemAlreadyExistsException">When lot already exists</exception>
        Task CreateLotAsync(Lot lot);

        /// <summary>
        /// Updates lot
        /// </summary>
        /// <param name="updated">Updated lot</param>
        /// <returns></returns>
        /// <exception cref="ItemNotFoundException">When lot not found</exception>
        Task UpdateLotAsync(Lot updated);
    }
}
