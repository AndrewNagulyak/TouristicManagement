namespace TravelManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB8 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tourists",
                c => new
                    {
                        Phone = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Email = c.String(),
                        Surname = c.String(),
                    })
                .PrimaryKey(t => t.Phone);
            
            AddColumn("dbo.Orders", "Tourist_Phone", c => c.String(maxLength: 128));
            CreateIndex("dbo.Orders", "Tourist_Phone");
            AddForeignKey("dbo.Orders", "Tourist_Phone", "dbo.Tourists", "Phone");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Tourist_Phone", "dbo.Tourists");
            DropIndex("dbo.Orders", new[] { "Tourist_Phone" });
            DropColumn("dbo.Orders", "Tourist_Phone");
            DropTable("dbo.Tourists");
        }
    }
}
