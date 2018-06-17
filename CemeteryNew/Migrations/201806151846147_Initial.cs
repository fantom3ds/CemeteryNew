namespace CemeteryNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BurialPlaces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NArea = c.Int(nullable: false),
                        NBurial = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Deceaseds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LName = c.String(nullable: false, maxLength: 50),
                        FName = c.String(nullable: false, maxLength: 20),
                        SName = c.String(nullable: false, maxLength: 50),
                        DOB = c.DateTime(),
                        DateDeath = c.DateTime(),
                        BurialPlaseId = c.Int(),
                        Photo = c.String(),
                        Description = c.String(maxLength: 200),
                        Confirmed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BurialPlaces", t => t.BurialPlaseId)
                .Index(t => t.BurialPlaseId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 50),
                        Password = c.Guid(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.CategoryDeceaseds",
                c => new
                    {
                        Category_Id = c.Int(nullable: false),
                        Deceased_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_Id, t.Deceased_Id })
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .ForeignKey("dbo.Deceaseds", t => t.Deceased_Id, cascadeDelete: true)
                .Index(t => t.Category_Id)
                .Index(t => t.Deceased_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.CategoryDeceaseds", "Deceased_Id", "dbo.Deceaseds");
            DropForeignKey("dbo.CategoryDeceaseds", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Deceaseds", "BurialPlaseId", "dbo.BurialPlaces");
            DropIndex("dbo.CategoryDeceaseds", new[] { "Deceased_Id" });
            DropIndex("dbo.CategoryDeceaseds", new[] { "Category_Id" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Deceaseds", new[] { "BurialPlaseId" });
            DropTable("dbo.CategoryDeceaseds");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Categories");
            DropTable("dbo.Deceaseds");
            DropTable("dbo.BurialPlaces");
        }
    }
}
