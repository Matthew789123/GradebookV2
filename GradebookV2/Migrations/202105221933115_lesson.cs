namespace GradebookV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lesson : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lessons", "Number", c => c.Int(nullable: false));
            AddColumn("dbo.Lessons", "ClassId", c => c.Int(nullable: false));
            CreateIndex("dbo.Lessons", "ClassId");
            AddForeignKey("dbo.Lessons", "ClassId", "dbo.Classes", "ClassId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lessons", "ClassId", "dbo.Classes");
            DropIndex("dbo.Lessons", new[] { "ClassId" });
            DropColumn("dbo.Lessons", "ClassId");
            DropColumn("dbo.Lessons", "Number");
        }
    }
}
