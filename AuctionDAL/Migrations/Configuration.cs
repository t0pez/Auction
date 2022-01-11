using System.Collections.Generic;
using System.Threading.Tasks;
using AuctionDAL.Models;
using AuctionDAL.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuctionDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<AuctionDAL.Context.AuctionContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override async void Seed(AuctionDAL.Context.AuctionContext context)
        {
            var userManager = new UserManager<User>(new UserStore<User>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            
            roleManager.Create(new IdentityRole { Name = "user" });
            roleManager.Create(new IdentityRole { Name = "admin" });

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "user",
                FirstName = "First",
                LastName = "Client",
                Wallet = new Wallet { Id = Guid.NewGuid() },
                OwnedLots = new List<Lot>(),
                LotsAsParticipant = new List<Lot>()
            };

            var admin = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "admin",
                FirstName = "First",
                LastName = "Admin",
                Wallet = new Wallet { Id = Guid.NewGuid() },
                OwnedLots = new List<Lot>(),
                LotsAsParticipant = new List<Lot>()
            };

            userManager.Create(user, "useruser");
            userManager.Create(admin, "adminadmin");

            userManager.AddToRole(user.Id, "user");
            userManager.AddToRole(admin.Id, "admin");

            await context.SaveChangesAsync();
        }
    }
}
