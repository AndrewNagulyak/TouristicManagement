namespace TravelManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InternalUserDatas", "User_Id", "dbo.Users");
            DropIndex("dbo.InternalUserDatas", new[] { "User_Id" });
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        State = c.Int(nullable: false),
                        Country_Name = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Name)
                .ForeignKey("dbo.Countries", t => t.Country_Name)
                .Index(t => t.Country_Name);
            
            DropColumn("dbo.InternalUserDatas", "Email");
            DropColumn("dbo.InternalUserDatas", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InternalUserDatas", "User_Id", c => c.Guid());
            AddColumn("dbo.InternalUserDatas", "Email", c => c.String());
            DropForeignKey("dbo.Cities", "Country_Name", "dbo.Countries");
            DropIndex("dbo.Cities", new[] { "Country_Name" });
            DropTable("dbo.Cities");
            DropTable("dbo.Countries");
            CreateIndex("dbo.InternalUserDatas", "User_Id");
            AddForeignKey("dbo.InternalUserDatas", "User_Id", "dbo.Users", "Id");
        }
    }
}
