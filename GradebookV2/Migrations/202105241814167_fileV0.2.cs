namespace GradebookV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fileV02 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        Content = c.Binary(nullable: false),
                        LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Lessons", t => t.LessonId, cascadeDelete: true)
                .Index(t => t.LessonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "LessonId", "dbo.Lessons");
            DropIndex("dbo.Files", new[] { "LessonId" });
            DropTable("dbo.Files");
        }
    }
}
