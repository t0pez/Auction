using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AuctionDAL.Context;
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
            var result = await Lots.FirstOrDefaultAsync(lot => lot.Id == id);

            if (result is null)
                throw new InvalidOperationException("Lot is not found");

            return result;
        }

        public async Task<bool> UpdateLotAsync(Lot updated)
        {
            var result = Lots.Attach(updated);
            _context.Entry(updated).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return result is not null;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}