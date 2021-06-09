namespace GradebookV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testsNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "Number", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "Number");
        }
    }
}
