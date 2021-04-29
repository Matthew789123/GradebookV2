namespace GradebookV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "Parent_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "Teacher_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SubjectClassTeachers", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.SubjectClassTeachers", "Teacher_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Grades", "Student_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "Parent_Id" });
            DropIndex("dbo.Messages", new[] { "Parent_Id" });
            DropIndex("dbo.Messages", new[] { "Teacher_Id" });
            DropIndex("dbo.SubjectClassTeachers", new[] { "SubjectId" });
            DropIndex("dbo.SubjectClassTeachers", new[] { "Teacher_Id" });
            DropIndex("dbo.Grades", new[] { "Student_Id" });
            DropColumn("dbo.AspNetUsers", "ParentId");
            DropColumn("dbo.Messages", "ParentId");
            DropColumn("dbo.Messages", "TeacherId");
            DropColumn("dbo.SubjectClassTeachers", "TeacherId");
            DropColumn("dbo.Grades", "StudentId");
            RenameColumn(table: "dbo.AspNetUsers", name: "Parent_Id", newName: "ParentId");
            RenameColumn(table: "dbo.Messages", name: "Parent_Id", newName: "ParentId");
            RenameColumn(table: "dbo.Messages", name: "Teacher_Id", newName: "TeacherId");
            RenameColumn(table: "dbo.SubjectClassTeachers", name: "Teacher_Id", newName: "TeacherId");
            RenameColumn(table: "dbo.Grades", name: "Student_Id", newName: "StudentId");
            AddColumn("dbo.SubjectClassTeachers", "Subject_SubjectId", c => c.Int());
            AlterColumn("dbo.Classes", "TeacherId", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "ParentId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Messages", "ParentId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Messages", "TeacherId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Messages", "ParentId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Messages", "TeacherId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.News", "TeacherId", c => c.String(nullable: false));
            AlterColumn("dbo.SubjectClassTeachers", "TeacherId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.SubjectClassTeachers", "SubjectId", c => c.String(nullable: false));
            AlterColumn("dbo.SubjectClassTeachers", "TeacherId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Grades", "StudentId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Grades", "StudentId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "ParentId");
            CreateIndex("dbo.Messages", "ParentId");
            CreateIndex("dbo.Messages", "TeacherId");
            CreateIndex("dbo.SubjectClassTeachers", "TeacherId");
            CreateIndex("dbo.SubjectClassTeachers", "Subject_SubjectId");
            CreateIndex("dbo.Grades", "StudentId");
            AddForeignKey("dbo.Messages", "ParentId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Messages", "TeacherId", "dbo.AspNetUsers", "Id", cascadeDelete: false);
            AddForeignKey("dbo.SubjectClassTeachers", "Subject_SubjectId", "dbo.Subjects", "SubjectId");
            AddForeignKey("dbo.SubjectClassTeachers", "TeacherId", "dbo.AspNetUsers", "Id", cascadeDelete: false);
            AddForeignKey("dbo.Grades", "StudentId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
           
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Grades", "StudentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SubjectClassTeachers", "TeacherId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SubjectClassTeachers", "Subject_SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Messages", "TeacherId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "ParentId", "dbo.AspNetUsers");
            DropIndex("dbo.Grades", new[] { "StudentId" });
            DropIndex("dbo.SubjectClassTeachers", new[] { "Subject_SubjectId" });
            DropIndex("dbo.SubjectClassTeachers", new[] { "TeacherId" });
            DropIndex("dbo.Messages", new[] { "TeacherId" });
            DropIndex("dbo.Messages", new[] { "ParentId" });
            DropIndex("dbo.AspNetUsers", new[] { "ParentId" });
            AlterColumn("dbo.Grades", "StudentId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Grades", "StudentId", c => c.Int(nullable: false));
            AlterColumn("dbo.SubjectClassTeachers", "TeacherId", c => c.String(maxLength: 128));
            AlterColumn("dbo.SubjectClassTeachers", "SubjectId", c => c.Int(nullable: false));
            AlterColumn("dbo.SubjectClassTeachers", "TeacherId", c => c.Int(nullable: false));
            AlterColumn("dbo.News", "TeacherId", c => c.Int(nullable: false));
            AlterColumn("dbo.Messages", "TeacherId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Messages", "ParentId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Messages", "TeacherId", c => c.Int(nullable: false));
            AlterColumn("dbo.Messages", "ParentId", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "ParentId", c => c.Int());
            AlterColumn("dbo.Classes", "TeacherId", c => c.Int(nullable: false));
            DropColumn("dbo.SubjectClassTeachers", "Subject_SubjectId");
            RenameColumn(table: "dbo.Grades", name: "StudentId", newName: "Student_Id");
            RenameColumn(table: "dbo.SubjectClassTeachers", name: "TeacherId", newName: "Teacher_Id");
            RenameColumn(table: "dbo.Messages", name: "TeacherId", newName: "Teacher_Id");
            RenameColumn(table: "dbo.Messages", name: "ParentId", newName: "Parent_Id");
            RenameColumn(table: "dbo.AspNetUsers", name: "ParentId", newName: "Parent_Id");
            AddColumn("dbo.Grades", "StudentId", c => c.Int(nullable: false));
            AddColumn("dbo.SubjectClassTeachers", "TeacherId", c => c.Int(nullable: false));
            AddColumn("dbo.Messages", "TeacherId", c => c.Int(nullable: false));
            AddColumn("dbo.Messages", "ParentId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "ParentId", c => c.Int());
            CreateIndex("dbo.Grades", "Student_Id");
            CreateIndex("dbo.SubjectClassTeachers", "Teacher_Id");
            CreateIndex("dbo.SubjectClassTeachers", "SubjectId");
            CreateIndex("dbo.Messages", "Teacher_Id");
            CreateIndex("dbo.Messages", "Parent_Id");
            CreateIndex("dbo.AspNetUsers", "Parent_Id");
            AddForeignKey("dbo.Grades", "Student_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.SubjectClassTeachers", "Teacher_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.SubjectClassTeachers", "SubjectId", "dbo.Subjects", "SubjectId", cascadeDelete: true);
            AddForeignKey("dbo.Messages", "Teacher_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Messages", "Parent_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
