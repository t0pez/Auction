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

        private DbSet<IndividualUser> Users => _context.IndividualUsers;

        public async Task<IEnumerable<IndividualUser>> GetAllUsersAsync()
        {
            return await Users.ToListAsync();
        }

        public async Task<IndividualUser> GetUserByIdAsync(Guid id)
        {
            var item = await Users.FirstOrDefaultAsync(user => user.Id == id);

            if (item is null)
                throw new ItemNotFoundException(nameof(item));

            return item;
        }

        public async Task CreateUserAsync(IndividualUser newUser)
        {
            if (await Users.AnyAsync(user => user.Id == newUser.Id))
                throw new ItemAlreadyExistsException(nameof(newUser));

            Users.Add(newUser);
        }

        public async Task UpdateUser(IndividualUser updated)
        {
            var item = await GetUserByIdAsync(updated.Id);
            
            _context.Entry(item).CurrentValues.SetValues(updated);
            
            await _context.SaveChangesAsync();
        }
    }
}