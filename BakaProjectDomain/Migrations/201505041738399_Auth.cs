namespace BakaProjectDomain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Auth : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tokens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        GeneratedTime = c.DateTime(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.Users", "AuthenticationId", c => c.String());
            AddColumn("dbo.Users", "TeacherAccount", c => c.Boolean());
            AddColumn("dbo.Users", "AuthenticationType", c => c.String());
            DropColumn("dbo.Users", "FacebookLink");
            DropColumn("dbo.Users", "FacebookId");
            DropColumn("dbo.Users", "Activated");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Activated", c => c.Boolean());
            AddColumn("dbo.Users", "FacebookId", c => c.String());
            AddColumn("dbo.Users", "FacebookLink", c => c.String());
            DropForeignKey("dbo.Tokens", "User_Id", "dbo.Users");
            DropIndex("dbo.Tokens", new[] { "User_Id" });
            DropColumn("dbo.Users", "AuthenticationType");
            DropColumn("dbo.Users", "TeacherAccount");
            DropColumn("dbo.Users", "AuthenticationId");
            DropTable("dbo.Tokens");
        }
    }
}
