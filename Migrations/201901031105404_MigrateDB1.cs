namespace TravelManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Site = c.String(),
                        Addres = c.String(),
                        Name = c.String(),
                        FoodState = c.Int(nullable: false),
                        Describe = c.String(),
                        Stars = c.Int(nullable: false),
                        HotelImage = c.Binary(),
                        City_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.City_Id)
                .Index(t => t.City_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Hotels", "City_Id", "dbo.Cities");
            DropIndex("dbo.Hotels", new[] { "City_Id" });
            DropTable("dbo.Hotels");
        }
    }
}
