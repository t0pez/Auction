using System.Threading.Tasks;
using AuctionDAL.Context;
using AuctionDAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuctionDAL.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AuctionContext _context;

        public UsersRepository(AuctionContext context)
        {
            _context = context;
            UserManager = new UserManager<User>(new UserStore<User>(_context));
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));
        }
        
        public UserManager<User> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}