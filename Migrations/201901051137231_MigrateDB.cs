namespace TravelManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Routes", "FromCity_Id", "dbo.Cities");
            DropIndex("dbo.Routes", new[] { "FromCity_Id" });
            AlterColumn("dbo.Routes", "FromCity_Id", c => c.Guid());
            CreateIndex("dbo.Routes", "FromCity_Id");
            AddForeignKey("dbo.Routes", "FromCity_Id", "dbo.Cities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Routes", "FromCity_Id", "dbo.Cities");
            DropIndex("dbo.Routes", new[] { "FromCity_Id" });
            AlterColumn("dbo.Routes", "FromCity_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.Routes", "FromCity_Id");
            AddForeignKey("dbo.Routes", "FromCity_Id", "dbo.Cities", "Id", cascadeDelete: true);
        }
    }
}
