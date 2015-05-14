namespace BakaProjectDomain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Resource : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Resources", "User_Id", "dbo.Users");
            DropIndex("dbo.Resources", new[] { "User_Id" });
            DropTable("dbo.Resources");
        }
    }
}
