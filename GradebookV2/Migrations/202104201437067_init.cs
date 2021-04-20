namespace GradebookV2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        ClassId = c.Int(nullable: false, identity: true),
                        Grade = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        HomeroomTeacher_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ClassId)
                .ForeignKey("dbo.AspNetUsers", t => t.HomeroomTeacher_Id, cascadeDelete: true)
                .Index(t => t.HomeroomTeacher_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Surname = c.String(),
                        BirthDate = c.DateTime(),
                        Sex = c.String(),
                        ParentId = c.Int(),
                        ClassId = c.Int(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Class_ClassId = c.Int(),
                        Parent_Id = c.String(maxLength: 128),
                        Class_ClassId1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classes", t => t.Class_ClassId)
                .ForeignKey("dbo.AspNetUsers", t => t.Parent_Id)
                .ForeignKey("dbo.Classes", t => t.Class_ClassId1)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Class_ClassId)
                .Index(t => t.Parent_Id)
                .Index(t => t.Class_ClassId1);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Content = c.String(nullable: false),
                        ParentId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        Parent_Id = c.String(maxLength: 128),
                        Teacher_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.AspNetUsers", t => t.Parent_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Teacher_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.Parent_Id)
                .Index(t => t.Teacher_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        NewsId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        Author_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.NewsId)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SubjectClassTeachers",
                c => new
                    {
                        SubjectClassTeacherId = c.Int(nullable: false, identity: true),
                        ClassId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                        SubjectId = c.Int(nullable: false),
                        Teacher_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.SubjectClassTeacherId)
                .ForeignKey("dbo.Classes", t => t.ClassId, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Teacher_Id)
                .Index(t => t.ClassId)
                .Index(t => t.SubjectId)
                .Index(t => t.Teacher_Id);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        SubjectId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.SubjectId);
            
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        GradeId = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Type = c.String(),
                        Comment = c.String(),
                        SubjectId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        Student_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.GradeId)
                .ForeignKey("dbo.AspNetUsers", t => t.Student_Id)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.SubjectId)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        TestId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        SubjectClassTeacherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TestId)
                .ForeignKey("dbo.SubjectClassTeachers", t => t.SubjectClassTeacherId, cascadeDelete: true)
                .Index(t => t.SubjectClassTeacherId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        AnswerA = c.String(nullable: false),
                        AnswerB = c.String(nullable: false),
                        AnswerC = c.String(nullable: false),
                        AnswerD = c.String(nullable: false),
                        CorrectAnswet = c.String(nullable: false),
                        TestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: true)
                .Index(t => t.TestId);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Content = c.Binary(nullable: false),
                        LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Lessons", t => t.LessonId, cascadeDelete: true)
                .Index(t => t.LessonId);
            
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        LessonId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Content = c.String(),
                        SubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LessonId)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Lessons", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Files", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Tests", "SubjectClassTeacherId", "dbo.SubjectClassTeachers");
            DropForeignKey("dbo.Questions", "TestId", "dbo.Tests");
            DropForeignKey("dbo.SubjectClassTeachers", "Teacher_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SubjectClassTeachers", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Grades", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Grades", "Student_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SubjectClassTeachers", "ClassId", "dbo.Classes");
            DropForeignKey("dbo.AspNetUsers", "Class_ClassId1", "dbo.Classes");
            DropForeignKey("dbo.Classes", "HomeroomTeacher_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Parent_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.News", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "Teacher_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "Parent_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Class_ClassId", "dbo.Classes");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Lessons", new[] { "SubjectId" });
            DropIndex("dbo.Files", new[] { "LessonId" });
            DropIndex("dbo.Questions", new[] { "TestId" });
            DropIndex("dbo.Tests", new[] { "SubjectClassTeacherId" });
            DropIndex("dbo.Grades", new[] { "Student_Id" });
            DropIndex("dbo.Grades", new[] { "SubjectId" });
            DropIndex("dbo.SubjectClassTeachers", new[] { "Teacher_Id" });
            DropIndex("dbo.SubjectClassTeachers", new[] { "SubjectId" });
            DropIndex("dbo.SubjectClassTeachers", new[] { "ClassId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.News", new[] { "Author_Id" });
            DropIndex("dbo.Messages", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Messages", new[] { "Teacher_Id" });
            DropIndex("dbo.Messages", new[] { "Parent_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Class_ClassId1" });
            DropIndex("dbo.AspNetUsers", new[] { "Parent_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Class_ClassId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Classes", new[] { "HomeroomTeacher_Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Lessons");
            DropTable("dbo.Files");
            DropTable("dbo.Questions");
            DropTable("dbo.Tests");
            DropTable("dbo.Grades");
            DropTable("dbo.Subjects");
            DropTable("dbo.SubjectClassTeachers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.News");
            DropTable("dbo.Messages");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Classes");
        }
    }
}
