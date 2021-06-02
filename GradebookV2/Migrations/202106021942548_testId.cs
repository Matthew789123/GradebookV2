namespace GradebookV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tests", "SubjectClassTeacherId", "dbo.SubjectClassTeachers");
            DropIndex("dbo.Tests", new[] { "SubjectClassTeacherId" });
            RenameColumn(table: "dbo.Tests", name: "SubjectClassTeacherId", newName: "SubjectClassTeacher_SubjectClassTeacherId");
            AddColumn("dbo.Tests", "SubjectId", c => c.Int(nullable: false));
            AddColumn("dbo.Tests", "ClassId", c => c.Int(nullable: false));
            AlterColumn("dbo.Tests", "SubjectClassTeacher_SubjectClassTeacherId", c => c.Int());
            CreateIndex("dbo.Tests", "SubjectId");
            CreateIndex("dbo.Tests", "ClassId");
            CreateIndex("dbo.Tests", "SubjectClassTeacher_SubjectClassTeacherId");
            AddForeignKey("dbo.Tests", "ClassId", "dbo.Classes", "ClassId", cascadeDelete: true);
            AddForeignKey("dbo.Tests", "SubjectId", "dbo.Subjects", "SubjectId", cascadeDelete: true);
            AddForeignKey("dbo.Tests", "SubjectClassTeacher_SubjectClassTeacherId", "dbo.SubjectClassTeachers", "SubjectClassTeacherId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tests", "SubjectClassTeacher_SubjectClassTeacherId", "dbo.SubjectClassTeachers");
            DropForeignKey("dbo.Tests", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Tests", "ClassId", "dbo.Classes");
            DropIndex("dbo.Tests", new[] { "SubjectClassTeacher_SubjectClassTeacherId" });
            DropIndex("dbo.Tests", new[] { "ClassId" });
            DropIndex("dbo.Tests", new[] { "SubjectId" });
            AlterColumn("dbo.Tests", "SubjectClassTeacher_SubjectClassTeacherId", c => c.Int(nullable: false));
            DropColumn("dbo.Tests", "ClassId");
            DropColumn("dbo.Tests", "SubjectId");
            RenameColumn(table: "dbo.Tests", name: "SubjectClassTeacher_SubjectClassTeacherId", newName: "SubjectClassTeacherId");
            CreateIndex("dbo.Tests", "SubjectClassTeacherId");
            AddForeignKey("dbo.Tests", "SubjectClassTeacherId", "dbo.SubjectClassTeachers", "SubjectClassTeacherId", cascadeDelete: true);
        }
    }
}
