namespace BakaProjectDomain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Share : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SharedExercises", newName: "Shares");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Shares", newName: "SharedExercises");
        }
    }
}
