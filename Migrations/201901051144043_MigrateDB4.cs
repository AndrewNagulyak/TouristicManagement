namespace TravelManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Routes", "Hotel_Id", "dbo.Hotels");
            DropIndex("dbo.Routes", new[] { "Hotel_Id" });
            AlterColumn("dbo.Routes", "Hotel_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.Routes", "Hotel_Id");
            AddForeignKey("dbo.Routes", "Hotel_Id", "dbo.Hotels", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Routes", "Hotel_Id", "dbo.Hotels");
            DropIndex("dbo.Routes", new[] { "Hotel_Id" });
            AlterColumn("dbo.Routes", "Hotel_Id", c => c.Guid());
            CreateIndex("dbo.Routes", "Hotel_Id");
            AddForeignKey("dbo.Routes", "Hotel_Id", "dbo.Hotels", "Id");
        }
    }
}
