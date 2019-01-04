namespace TravelManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hotels", "Image", c => c.String());
            DropColumn("dbo.Hotels", "HotelImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Hotels", "HotelImage", c => c.Binary());
            DropColumn("dbo.Hotels", "Image");
        }
    }
}
