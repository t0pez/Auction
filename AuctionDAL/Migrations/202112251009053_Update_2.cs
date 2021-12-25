namespace AuctionDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Lots", "ProlongationTime", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Lots", "TimeForStep", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Lots", "TimeForStep", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Lots", "ProlongationTime", c => c.DateTime(nullable: false));
        }
    }
}
