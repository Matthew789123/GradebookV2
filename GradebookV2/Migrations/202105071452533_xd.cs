namespace GradebookV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SubjectClassTeachers", "Subject_SubjectId", "dbo.Subjects");
            DropIndex("dbo.SubjectClassTeachers", new[] { "Subject_SubjectId" });
            DropColumn("dbo.SubjectClassTeachers", "SubjectId");
            RenameColumn(table: "dbo.SubjectClassTeachers", name: "Subject_SubjectId", newName: "SubjectId");
            AlterColumn("dbo.SubjectClassTeachers", "SubjectId", c => c.Int(nullable: false));
            AlterColumn("dbo.SubjectClassTeachers", "SubjectId", c => c.Int(nullable: false));
            CreateIndex("dbo.SubjectClassTeachers", "SubjectId");
            AddForeignKey("dbo.SubjectClassTeachers", "SubjectId", "dbo.Subjects", "SubjectId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubjectClassTeachers", "SubjectId", "dbo.Subjects");
            DropIndex("dbo.SubjectClassTeachers", new[] { "SubjectId" });
            AlterColumn("dbo.SubjectClassTeachers", "SubjectId", c => c.Int());
            AlterColumn("dbo.SubjectClassTeachers", "SubjectId", c => c.String(nullable: false));
            RenameColumn(table: "dbo.SubjectClassTeachers", name: "SubjectId", newName: "Subject_SubjectId");
            AddColumn("dbo.SubjectClassTeachers", "SubjectId", c => c.String(nullable: false));
            CreateIndex("dbo.SubjectClassTeachers", "Subject_SubjectId");
            AddForeignKey("dbo.SubjectClassTeachers", "Subject_SubjectId", "dbo.Subjects", "SubjectId");
        }
    }
}
