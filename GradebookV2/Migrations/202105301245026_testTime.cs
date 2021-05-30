namespace GradebookV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tests", "start", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tests", "duration", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tests", "duration");
            DropColumn("dbo.Tests", "start");
        }
    }
}
