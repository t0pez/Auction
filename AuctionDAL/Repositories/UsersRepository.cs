using AuctionDAL.Context;
using AuctionDAL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuctionDAL.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        public UsersRepository(AuctionContext context)
        {
            UserManager = new UserManager<User>(new UserStore<User>(context));
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        }
        
        public UserManager<User> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }
    }
}