using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AuctionDAL.Context;
using AuctionDAL.Enums;
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

        public async Task<IEnumerable<Lot>> GetAllCreatedLotsAsync()
        {
            return await Lots.Where(lot => lot.Status == LotStatus.Created).ToListAsync();
        }
        
        public async Task<IEnumerable<Lot>> GetAllOpenedLotsAsync()
        {
            return await Lots.Where(lot => lot.Status == LotStatus.Opened).ToListAsync();
        }

        public async Task<IEnumerable<Lot>> GetAllClosedLotsAsync()
        {
            return await Lots.Where(lot => lot.Status == LotStatus.Closed).ToListAsync();
        }

        public async Task<Lot> GetLotByIdAsync(Guid id)
        {
            var item = await Lots.FirstOrDefaultAsync(lot => lot.Id == id);

            if (item is null)
                throw new ItemNotFoundException(nameof(item));

            return item;
        }

        public async Task CreateLotAsync(Lot lot)
        {
            if (await Lots.AnyAsync(l => l.Id == lot.Id))
                throw new ItemAlreadyExistsException(nameof(lot));

            Lots.Add(lot);

            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateLotAsync(Lot updated)
        {
            var item = await GetLotByIdAsync(updated.Id);

            _context.Entry(item).CurrentValues.SetValues(updated);
            
            await _context.SaveChangesAsync();
        }
    }
}
