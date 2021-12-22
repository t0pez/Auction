using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using AuctionDAL.Context;
using AuctionDAL.Exceptions;
using AuctionDAL.Models;

namespace AuctionDAL.Repositories
{
    public class EntityUserRepository : IUserRepository<EntityUser>
    {
        private readonly AuctionContext _context;

        public EntityUserRepository(AuctionContext context)
        {
            _context = context;
        }

        private DbSet<EntityUser> Users => _context.Entities;

        public async Task<IEnumerable<EntityUser>> GetAllUsersAsync()
        {
            return await Users.ToListAsync();
        }

        public async Task<EntityUser> FindByIdAsync(string id)
        {
            var item = await Users.FirstOrDefaultAsync(user => user.Id == id);

            if (item is null)
                throw new ItemNotFoundException(nameof(item));

            return item;
        }

        public async Task<EntityUser> FindByNameAsync(string userName)
        {
            var item = await Users.FirstOrDefaultAsync(user => user.UserName == userName);

            if (item is null)
                throw new ItemNotFoundException(nameof(item));

            return item;
        }

        public async Task CreateAsync(EntityUser newUser)
        {
            if (await Users.AnyAsync(user => user.Id == newUser.Id))
                throw new ItemAlreadyExistsException(nameof(newUser));

            Users.Add(newUser);
        }

        public async Task UpdateAsync(EntityUser updated)
        {
            var item = await FindByIdAsync(updated.Id);

            _context.Entry(item).CurrentValues.SetValues(updated);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(EntityUser user)
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