namespace TravelManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Routes", "ToCity_Id", "dbo.Cities");
            DropIndex("dbo.Routes", new[] { "ToCity_Id" });
            AlterColumn("dbo.Routes", "ToCity_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.Routes", "ToCity_Id");
            AddForeignKey("dbo.Routes", "ToCity_Id", "dbo.Cities", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Routes", "ToCity_Id", "dbo.Cities");
            DropIndex("dbo.Routes", new[] { "ToCity_Id" });
            AlterColumn("dbo.Routes", "ToCity_Id", c => c.Guid());
            CreateIndex("dbo.Routes", "ToCity_Id");
            AddForeignKey("dbo.Routes", "ToCity_Id", "dbo.Cities", "Id");
        }
    }
}
