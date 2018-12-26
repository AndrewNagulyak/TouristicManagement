namespace TravelManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Username = c.String(),
                        Role = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InternalUserDatas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Username = c.String(),
                        Email = c.String(),
                        HashedPassword = c.String(),
                        Role = c.String(),
                        User_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InternalUserDatas", "User_Id", "dbo.Users");
            DropIndex("dbo.InternalUserDatas", new[] { "User_Id" });
            DropTable("dbo.InternalUserDatas");
            DropTable("dbo.Users");
        }
    }
}
