namespace BakaProjectDomain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "FaceBookId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "FaceBookId", c => c.Int(nullable: false));
        }
    }
}
