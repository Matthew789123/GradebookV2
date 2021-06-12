namespace GradebookV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gradeName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Grades", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Grades", "Name");
        }
    }
}
