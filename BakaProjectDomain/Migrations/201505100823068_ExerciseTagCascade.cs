namespace BakaProjectDomain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExerciseTagCascade : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tags", "Exercise_Id", "dbo.Exercises");
            DropIndex("dbo.Tags", new[] { "Exercise_Id" });
            AlterColumn("dbo.Tags", "Exercise_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Tags", "Exercise_Id");
            AddForeignKey("dbo.Tags", "Exercise_Id", "dbo.Exercises", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "Exercise_Id", "dbo.Exercises");
            DropIndex("dbo.Tags", new[] { "Exercise_Id" });
            AlterColumn("dbo.Tags", "Exercise_Id", c => c.Int());
            CreateIndex("dbo.Tags", "Exercise_Id");
            AddForeignKey("dbo.Tags", "Exercise_Id", "dbo.Exercises", "Id");
        }
    }
}
