namespace GradebookV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class questionPoints : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "CorrectAnswer", c => c.String(nullable: false));
            AddColumn("dbo.Questions", "Points", c => c.Int(nullable: false));
            DropColumn("dbo.Questions", "CorrectAnswet");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "CorrectAnswet", c => c.String(nullable: false));
            DropColumn("dbo.Questions", "Points");
            DropColumn("dbo.Questions", "CorrectAnswer");
        }
    }
}
