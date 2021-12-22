using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using AuctionDAL.Context;
using AuctionDAL.Exceptions;
using AuctionDAL.Models;

namespace AuctionDAL.Repositories
{
    public class IndividualUserRepository : IUserRepository<IndividualUser>
    {
        private readonly AuctionContext _context;

        public IndividualUserRepository(AuctionContext context)
        {
            _context = context;
        }

        private DbSet<IndividualUser> Users => _context.Individuals;

        public async Task<IEnumerable<IndividualUser>> GetAllUsersAsync()
        {
            return await Users.ToListAsync();
        }

        public async Task<IndividualUser> FindByIdAsync(string id)
        {
            var item = await Users.FirstOrDefaultAsync(user => user.Id == id);

            if (item is null)
                throw new ItemNotFoundException(nameof(item));

            return item;
        }

        public async Task<IndividualUser> FindByNameAsync(string name)
        {
            var item = await Users.FirstOrDefaultAsync(user => user.UserName == name);

            if (item is null)
                throw new ItemNotFoundException(nameof(item));

            return item;
        }

        public async Task CreateAsync(IndividualUser newUser)
        {
            if (await Users.AnyAsync(user => user.Id == newUser.Id))
                throw new ItemAlreadyExistsException(nameof(newUser));

            Users.Add(newUser);
        }

        public async Task UpdateAsync(IndividualUser updated)
        {
            var item = await FindByIdAsync(updated.Id);

            _context.Entry(item).CurrentValues.SetValues(updated);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(IndividualUser user)
        {
            var item = await FindByIdAsync(user.Id);

            _context.Entry(item).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}