namespace BakaProjectDomain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FacebookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Exercises", "Title", c => c.String());
            AddColumn("dbo.Exercises", "Question", c => c.String());
            AddColumn("dbo.Exercises", "Answer", c => c.String());
            AddColumn("dbo.Exercises", "Tip", c => c.String());
            AddColumn("dbo.Exercises", "User_Id", c => c.Int());
            CreateIndex("dbo.Exercises", "User_Id");
            AddForeignKey("dbo.Exercises", "User_Id", "dbo.Users", "Id");
            DropColumn("dbo.Exercises", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Exercises", "Name", c => c.String());
            DropForeignKey("dbo.Exercises", "User_Id", "dbo.Users");
            DropIndex("dbo.Exercises", new[] { "User_Id" });
            DropColumn("dbo.Exercises", "User_Id");
            DropColumn("dbo.Exercises", "Tip");
            DropColumn("dbo.Exercises", "Answer");
            DropColumn("dbo.Exercises", "Question");
            DropColumn("dbo.Exercises", "Title");
            DropTable("dbo.Users");
        }
    }
}
