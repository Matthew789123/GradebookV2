namespace GradebookV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Classes", "HomeroomTeacher_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Classes", new[] { "HomeroomTeacher_Id" });
            AlterColumn("dbo.Classes", "TeacherId", c => c.String());
            AlterColumn("dbo.Classes", "HomeroomTeacher_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Classes", "HomeroomTeacher_Id");
            AddForeignKey("dbo.Classes", "HomeroomTeacher_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Classes", "HomeroomTeacher_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Classes", new[] { "HomeroomTeacher_Id" });
            AlterColumn("dbo.Classes", "HomeroomTeacher_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Classes", "TeacherId", c => c.String(nullable: false));
            CreateIndex("dbo.Classes", "HomeroomTeacher_Id");
            AddForeignKey("dbo.Classes", "HomeroomTeacher_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
