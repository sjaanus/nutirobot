namespace BakaProjectDomain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tags : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Exercise_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exercises", t => t.Exercise_Id)
                .Index(t => t.Exercise_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "Exercise_Id", "dbo.Exercises");
            DropIndex("dbo.Tags", new[] { "Exercise_Id" });
            DropTable("dbo.Tags");
        }
    }
}
