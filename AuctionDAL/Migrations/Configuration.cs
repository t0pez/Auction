using System.Collections.Generic;
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
            var lotsRepository = new LotsRepository(context);

            roleManager.Create(new IdentityRole { Name = "user" });
            roleManager.Create(new IdentityRole { Name = "admin" });

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "user",
                FirstName = "First",
                LastName = "User",
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

            var lot = new Lot
            {
                Id = Guid.NewGuid(),
                Status = 0,
                Name = "First lot",
                Description = "Some description for the lot",

                StartPrice = new Money { Id = Guid.NewGuid(), Amount = 1_000, Currency = 1 },
                HighestPrice = new Money { Id = Guid.NewGuid() },
                MinStepPrice = new Money { Id = Guid.NewGuid(), Amount = 100, Currency = 1 },

                DateOfCreation = DateTime.Now,
                StartDate = null,
                EndDate = null,
                ProlongationTime = new TimeSpan(0, 10, 0),
                TimeForStep = new TimeSpan(0, 10, 0),

                Owner = user,
                Acquirer = null,
                Participants = new List<User>(),
            };

            lot.HighestPrice.Currency = lot.StartPrice.Currency;
            lot.HighestPrice.Amount = lot.StartPrice.Amount;

            await lotsRepository.CreateLotAsync(lot);

            await context.SaveChangesAsync();
        }
    }
}
