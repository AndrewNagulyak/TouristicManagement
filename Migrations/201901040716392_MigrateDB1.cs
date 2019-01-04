namespace TravelManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cities", "Km", c => c.Int(nullable: false));
            AddColumn("dbo.Hotels", "PricePerNight", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hotels", "PricePerNight");
            DropColumn("dbo.Cities", "Km");
        }
    }
}
