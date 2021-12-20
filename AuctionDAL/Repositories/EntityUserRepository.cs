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

        private DbSet<EntityUser> Users => _context.EntityUsers;

        public async Task<IEnumerable<EntityUser>> GetAllUsersAsync()
        {
            return await Users.ToListAsync();
        }

        public async Task<EntityUser> GetUserByIdAsync(Guid id)
        {
            return await GetUserByIdAsync(id.ToString());
        }
        
        public async Task<EntityUser> GetUserByIdAsync(string id)
        {
            var item = await Users.FirstOrDefaultAsync(user => user.Id == id);

            if (item is null)
                throw new ItemNotFoundException(nameof(item));

            return item;
        }

        public async Task CreateUserAsync(EntityUser newUser)
        {
            if (await Users.AnyAsync(user => user.Id == newUser.Id))
                throw new ItemAlreadyExistsException(nameof(newUser));

            Users.Add(newUser);
        }

        public async Task UpdateUser(EntityUser updated)
        {
            var item = await GetUserByIdAsync(updated.Id);
            
            _context.Entry(item).CurrentValues.SetValues(updated);
            
            await _context.SaveChangesAsync();
        }
    }
}