using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AuctionDAL.Context;
using AuctionDAL.Exceptions;
using AuctionDAL.Models;

namespace AuctionDAL.Repositories
{
    public class LotsRepository : ILotsRepository
    {
        private readonly AuctionContext _context;

        public LotsRepository(AuctionContext context)
        {
            _context = context;
        }

        private DbSet<Lot> Lots => _context.Lots;

        public async Task<IEnumerable<Lot>> GetAllLotsAsync()
        {
            return await Lots.ToListAsync();
        }

        public async Task<IEnumerable<Lot>> GetAllOpenLotsAsync()
        {
            return await Lots.Where(lot => lot.IsOpen).ToListAsync();
        }

        public async Task<IEnumerable<Lot>> GetAllClosedLotsAsync()
        {
            return await Lots.Where(lot => lot.IsOpen == false).ToListAsync();
        }

        public async Task<Lot> GetLotByIdAsync(Guid id)
        {
            var item = await Lots.FirstOrDefaultAsync(lot => lot.Id == id);

            if (item is null)
                throw new ItemNotFoundException(nameof(item));

            return item;
        }

        public async Task UpdateLotAsync(Lot updated)
        {
            var item = await GetLotByIdAsync(updated.Id);

            _context.Entry(item).CurrentValues.SetValues(updated);
            
            await _context.SaveChangesAsync();
        }
    }
}
