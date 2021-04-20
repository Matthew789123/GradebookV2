using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Gradebook.Models
{
    public class GradebookDbContext : DbContext
    {
        public GradebookDbContext() : base("GradebookCS") { }


        public DbSet<Class> Classes { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<SubjectClassTeacher> SubjectClassTeacher { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<User> Users { get; set; }
    }
}