namespace BakaProjectDomain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Share1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shares", "Hash", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shares", "Hash");
        }
    }
}
