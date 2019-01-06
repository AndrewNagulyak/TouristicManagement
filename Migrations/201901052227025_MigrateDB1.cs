namespace TravelManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB1 : DbMigration
    {
        public override void Up()
        {
            
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RouteId = c.Guid(nullable: false),
                        TouristId = c.Guid(nullable: false),
                        Phone = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Routes", t => t.RouteId, cascadeDelete: true)
                .ForeignKey("dbo.Tourists", t => new { t.TouristId, t.Phone })
                .Index(t => t.RouteId)
                .Index(t => new { t.TouristId, t.Phone });
            
            CreateTable(
                "dbo.Tourists",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Phone = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Email = c.String(),
                        Surname = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.Phone });
            
         
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Routes", "ToCity_Id", "dbo.Cities");
            DropForeignKey("dbo.Orders", new[] { "TouristId", "Phone" }, "dbo.Tourists");
            DropForeignKey("dbo.Orders", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.Routes", "Hotel_Id", "dbo.Hotels");
            DropForeignKey("dbo.Routes", "FromCity_Id", "dbo.Cities");
            DropForeignKey("dbo.Hotels", "City_Id", "dbo.Cities");
            DropForeignKey("dbo.Cities", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Orders", new[] { "TouristId", "Phone" });
            DropIndex("dbo.Orders", new[] { "RouteId" });
            DropIndex("dbo.Routes", new[] { "ToCity_Id" });
            DropIndex("dbo.Routes", new[] { "Hotel_Id" });
            DropIndex("dbo.Routes", new[] { "FromCity_Id" });
            DropIndex("dbo.Hotels", new[] { "City_Id" });
            DropIndex("dbo.Cities", new[] { "Country_Id" });
            DropTable("dbo.InternalUserDatas");
            DropTable("dbo.Users");
            DropTable("dbo.Tourists");
            DropTable("dbo.Orders");
            DropTable("dbo.Routes");
            DropTable("dbo.Hotels");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
        }
    }
}
