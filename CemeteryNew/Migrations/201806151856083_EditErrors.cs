namespace CemeteryNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditErrors : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Roles", "Name", c => c.String());
            DropColumn("dbo.Roles", "RoleName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Roles", "RoleName", c => c.String());
            DropColumn("dbo.Roles", "Name");
        }
    }
}
