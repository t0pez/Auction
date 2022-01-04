namespace AuctionDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cuttinglogs : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AspNetUsers", newName: "Users");
            DropForeignKey("dbo.Lots", "Buyer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Lots", new[] { "User_Id" });
            DropIndex("dbo.Lots", new[] { "User_Id1" });
            DropIndex("dbo.Lots", new[] { "Buyer_Id" });
            DropIndex("dbo.Lots", new[] { "Owner_Id" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            RenameColumn(table: "dbo.Lots", name: "Owner_Id", newName: "OwnerId");
            RenameColumn(table: "dbo.Lots", name: "User_Id", newName: "AcquirerId");
            CreateTable(
                "dbo.LotParticipant",
                c => new
                    {
                        LotRefId = c.Guid(nullable: false),
                        UserRefId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LotRefId, t.UserRefId })
                .ForeignKey("dbo.Lots", t => t.LotRefId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserRefId, cascadeDelete: true)
                .Index(t => t.LotRefId)
                .Index(t => t.UserRefId);
            
            AlterColumn("dbo.Lots", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Lots", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Lots", "AcquirerId", c => c.String(nullable: true, maxLength: 128));
            AlterColumn("dbo.Lots", "OwnerId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Users", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "UserName", c => c.String());
            AlterColumn("dbo.AspNetUserClaims", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Lots", "OwnerId");
            CreateIndex("dbo.Lots", "AcquirerId");
            CreateIndex("dbo.AspNetUserClaims", "UserId");
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.Users", "Id");
            DropColumn("dbo.Lots", "Buyer_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lots", "Buyer_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.LotParticipant", "UserRefId", "dbo.Users");
            DropForeignKey("dbo.LotParticipant", "LotRefId", "dbo.Lots");
            DropIndex("dbo.LotParticipant", new[] { "UserRefId" });
            DropIndex("dbo.LotParticipant", new[] { "LotRefId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.Lots", new[] { "AcquirerId" });
            DropIndex("dbo.Lots", new[] { "OwnerId" });
            AlterColumn("dbo.AspNetUserClaims", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Users", "Email", c => c.String(maxLength: 256));
            AlterColumn("dbo.Users", "LastName", c => c.String());
            AlterColumn("dbo.Users", "FirstName", c => c.String());
            AlterColumn("dbo.Lots", "OwnerId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Lots", "AcquirerId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Lots", "Description", c => c.String());
            AlterColumn("dbo.Lots", "Name", c => c.String());
            DropTable("dbo.LotParticipant");
            RenameColumn(table: "dbo.Lots", name: "AcquirerId", newName: "User_Id");
            RenameColumn(table: "dbo.Lots", name: "OwnerId", newName: "User_Id1");
            RenameColumn(table: "dbo.Lots", name: "OwnerId", newName: "Owner_Id");
            CreateIndex("dbo.AspNetUserClaims", "UserId");
            CreateIndex("dbo.Users", "UserName", unique: true, name: "UserNameIndex");
            CreateIndex("dbo.Lots", "Owner_Id");
            CreateIndex("dbo.Lots", "Buyer_Id");
            CreateIndex("dbo.Lots", "User_Id1");
            CreateIndex("dbo.Lots", "User_Id");
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Lots", "Buyer_Id", "dbo.AspNetUsers", "Id");
            RenameTable(name: "dbo.Users", newName: "AspNetUsers");
        }
    }
}
