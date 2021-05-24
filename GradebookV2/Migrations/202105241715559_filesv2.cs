namespace GradebookV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class filesv2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Files", "LessonId", "dbo.Lessons");
            DropIndex("dbo.Files", new[] { "LessonId" });
            DropTable("dbo.Files");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Content = c.Binary(nullable: false),
                        LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileId);
            
            CreateIndex("dbo.Files", "LessonId");
            AddForeignKey("dbo.Files", "LessonId", "dbo.Lessons", "LessonId", cascadeDelete: true);
        }
    }
}
