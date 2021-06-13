namespace GradebookV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class messagefix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserMessages", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Messages", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.UserMessages", "Date");
        }
    }
}
