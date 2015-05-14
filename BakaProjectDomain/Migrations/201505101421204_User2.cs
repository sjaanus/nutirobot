namespace BakaProjectDomain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "UserLink");
        }
    }
}
