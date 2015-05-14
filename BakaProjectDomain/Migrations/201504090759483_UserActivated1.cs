namespace BakaProjectDomain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserActivated1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Activated", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Activated", c => c.Boolean(nullable: false));
        }
    }
}
