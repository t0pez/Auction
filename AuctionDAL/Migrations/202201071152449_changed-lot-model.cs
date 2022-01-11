namespace AuctionDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedlotmodel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Lots", "StartDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Lots", "TimeForStep");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lots", "TimeForStep", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Lots", "StartDate", c => c.DateTime());
        }
    }
}
