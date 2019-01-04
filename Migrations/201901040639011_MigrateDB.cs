
namespace TravelManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CityName = c.String(),
                        State = c.Int(nullable: false),
                        Country_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CountryName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        TouristAmount = c.Int(nullable: false),
                        type = c.Int(nullable: false),
                        transport = c.Int(nullable: false),
                        FinishDate = c.DateTime(nullable: false),
                        Price = c.Int(nullable: false),
                        FromCity_Id = c.Guid(),
                        Hotel_Id = c.Guid(),
                        ToCity_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.FromCity_Id)
                .ForeignKey("dbo.Hotels", t => t.Hotel_Id)
                .ForeignKey("dbo.Cities", t => t.ToCity_Id)
                .Index(t => t.FromCity_Id)
                .Index(t => t.Hotel_Id)
                .Index(t => t.ToCity_Id);
            

            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Routes", "ToCity_Id", "dbo.Cities");
            DropForeignKey("dbo.Routes", "Hotel_Id", "dbo.Hotels");
            DropForeignKey("dbo.Routes", "FromCity_Id", "dbo.Cities");
            DropForeignKey("dbo.Hotels", "City_Id", "dbo.Cities");
            DropForeignKey("dbo.Cities", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Routes", new[] { "ToCity_Id" });
            DropIndex("dbo.Routes", new[] { "Hotel_Id" });
            DropIndex("dbo.Routes", new[] { "FromCity_Id" });
            DropIndex("dbo.Hotels", new[] { "City_Id" });
            DropIndex("dbo.Cities", new[] { "Country_Id" });
            DropTable("dbo.InternalUserDatas");
            DropTable("dbo.Users");
            DropTable("dbo.Routes");
            DropTable("dbo.Hotels");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
        }
    }
}
