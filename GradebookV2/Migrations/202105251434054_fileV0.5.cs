namespace GradebookV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fileV05 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "FileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Files", "FileName");
        }
    }
}
