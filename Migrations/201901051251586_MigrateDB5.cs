namespace TravelManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Routes", "FromCity_Id", "dbo.Cities");
            DropForeignKey("dbo.Routes", "Hotel_Id", "dbo.Hotels");
            DropForeignKey("dbo.Routes", "ToCity_Id", "dbo.Cities");
            DropIndex("dbo.Routes", new[] { "FromCity_Id" });
            DropIndex("dbo.Routes", new[] { "Hotel_Id" });
            DropIndex("dbo.Routes", new[] { "ToCity_Id" });
            AlterColumn("dbo.Routes", "FromCity_Id", c => c.Guid());
            AlterColumn("dbo.Routes", "Hotel_Id", c => c.Guid());
            AlterColumn("dbo.Routes", "ToCity_Id", c => c.Guid());
            CreateIndex("dbo.Routes", "FromCity_Id");
            CreateIndex("dbo.Routes", "Hotel_Id");
            CreateIndex("dbo.Routes", "ToCity_Id");
            AddForeignKey("dbo.Routes", "FromCity_Id", "dbo.Cities", "Id");
            AddForeignKey("dbo.Routes", "Hotel_Id", "dbo.Hotels", "Id");
            AddForeignKey("dbo.Routes", "ToCity_Id", "dbo.Cities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Routes", "ToCity_Id", "dbo.Cities");
            DropForeignKey("dbo.Routes", "Hotel_Id", "dbo.Hotels");
            DropForeignKey("dbo.Routes", "FromCity_Id", "dbo.Cities");
            DropIndex("dbo.Routes", new[] { "ToCity_Id" });
            DropIndex("dbo.Routes", new[] { "Hotel_Id" });
            DropIndex("dbo.Routes", new[] { "FromCity_Id" });
            AlterColumn("dbo.Routes", "ToCity_Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Routes", "Hotel_Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Routes", "FromCity_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.Routes", "ToCity_Id");
            CreateIndex("dbo.Routes", "Hotel_Id");
            CreateIndex("dbo.Routes", "FromCity_Id");
            AddForeignKey("dbo.Routes", "ToCity_Id", "dbo.Cities", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Routes", "Hotel_Id", "dbo.Hotels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Routes", "FromCity_Id", "dbo.Cities", "Id", cascadeDelete: true);
        }
    }
}
