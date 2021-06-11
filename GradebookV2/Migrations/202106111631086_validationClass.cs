namespace GradebookV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validationClass : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Classes", "Name", c => c.String(nullable: false, maxLength: 1));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Classes", "Name", c => c.String(nullable: false));
        }
    }
}
