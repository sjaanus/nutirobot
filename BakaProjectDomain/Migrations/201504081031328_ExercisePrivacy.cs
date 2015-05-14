namespace BakaProjectDomain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExercisePrivacy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exercises", "Privacy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exercises", "Privacy");
        }
    }
}
