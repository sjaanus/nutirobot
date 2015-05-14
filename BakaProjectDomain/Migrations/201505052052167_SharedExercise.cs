namespace BakaProjectDomain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SharedExercise : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SharedExercises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Question = c.String(),
                        Answer = c.String(),
                        Tip = c.String(),
                        Tags = c.String(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SharedExercises", "User_Id", "dbo.Users");
            DropIndex("dbo.SharedExercises", new[] { "User_Id" });
            DropTable("dbo.SharedExercises");
        }
    }
}
