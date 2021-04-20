using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GradebookV2.Models
{
    // Możesz dodać dane profilu dla użytkownika, dodając więcej właściwości do klasy ApplicationUser. Odwiedź stronę https://go.microsoft.com/fwlink/?LinkID=317594, aby dowiedzieć się więcej.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Element authenticationType musi pasować do elementu zdefiniowanego w elemencie CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Dodaj tutaj niestandardowe oświadczenia użytkownika
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("GradebookCS", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

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