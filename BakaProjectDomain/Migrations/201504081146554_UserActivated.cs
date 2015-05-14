namespace BakaProjectDomain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserActivated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Activated", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Activated");
        }
    }
}
