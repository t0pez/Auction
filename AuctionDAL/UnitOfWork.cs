using System.Threading.Tasks;
using AuctionDAL.Context;
using AuctionDAL.Models;
using AuctionDAL.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuctionDAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuctionContext _context;
        private readonly ILotsRepository _lotsRepository;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UnitOfWork(AuctionContext context)
        {
            _context = context;
            _lotsRepository = new LotsRepository(_context);
            _userManager = new UserManager<User>(new UserStore<User>(_context));
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context));
        }

        public ILotsRepository LotsRepository => _lotsRepository;

        public UserManager<User> UserManager => _userManager;

        public RoleManager<IdentityRole> RoleManager => _roleManager;

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}