﻿namespace AuctionDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inital : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Wallet_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Wallets", t => t.Wallet_Id)
                .Index(t => t.Wallet_Id);
            
            CreateTable(
                "dbo.Lots",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Status = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        DateOfCreation = c.DateTime(nullable: false),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        ProlongationTime = c.DateTime(nullable: false),
                        TimeForStep = c.DateTime(nullable: false),
                        HighestPrice_Id = c.Guid(),
                        MinStepPrice_Id = c.Guid(),
                        User_Id = c.Guid(),
                        User_Id1 = c.Guid(),
                        Owner_Id = c.Guid(),
                        StartPrice_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Moneys", t => t.HighestPrice_Id)
                .ForeignKey("dbo.Moneys", t => t.MinStepPrice_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Users", t => t.User_Id1)
                .ForeignKey("dbo.Users", t => t.Owner_Id)
                .ForeignKey("dbo.Moneys", t => t.StartPrice_Id)
                .Index(t => t.HighestPrice_Id)
                .Index(t => t.MinStepPrice_Id)
                .Index(t => t.User_Id)
                .Index(t => t.User_Id1)
                .Index(t => t.Owner_Id)
                .Index(t => t.StartPrice_Id);
            
            CreateTable(
                "dbo.Moneys",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Currency = c.Int(nullable: false),
                        Wallet_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Wallets", t => t.Wallet_Id)
                .Index(t => t.Wallet_Id);
            
            CreateTable(
                "dbo.Wallets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lots", "StartPrice_Id", "dbo.Moneys");
            DropForeignKey("dbo.Lots", "Owner_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Wallet_Id", "dbo.Wallets");
            DropForeignKey("dbo.Moneys", "Wallet_Id", "dbo.Wallets");
            DropForeignKey("dbo.Lots", "User_Id1", "dbo.Users");
            DropForeignKey("dbo.Lots", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Lots", "MinStepPrice_Id", "dbo.Moneys");
            DropForeignKey("dbo.Lots", "HighestPrice_Id", "dbo.Moneys");
            DropIndex("dbo.Moneys", new[] { "Wallet_Id" });
            DropIndex("dbo.Lots", new[] { "StartPrice_Id" });
            DropIndex("dbo.Lots", new[] { "Owner_Id" });
            DropIndex("dbo.Lots", new[] { "User_Id1" });
            DropIndex("dbo.Lots", new[] { "User_Id" });
            DropIndex("dbo.Lots", new[] { "MinStepPrice_Id" });
            DropIndex("dbo.Lots", new[] { "HighestPrice_Id" });
            DropIndex("dbo.Users", new[] { "Wallet_Id" });
            DropTable("dbo.Wallets");
            DropTable("dbo.Moneys");
            DropTable("dbo.Lots");
            DropTable("dbo.Users");
        }
    }
}
