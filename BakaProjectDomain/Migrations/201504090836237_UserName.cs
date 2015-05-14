namespace BakaProjectDomain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Name", c => c.String());
            AddColumn("dbo.Users", "FacebookLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "FacebookLink");
            DropColumn("dbo.Users", "Name");
        }
    }
}
