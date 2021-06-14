using GradebookV2.Models;
using Hangfire;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

[assembly: OwinStartupAttribute(typeof(GradebookV2.Startup))]
namespace GradebookV2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            GlobalConfiguration.Configuration
                .UseSqlServerStorage("GradebookCS");
            
            RecurringJob.AddOrUpdate<EmailSender>(x => x.SendMail(),Cron.Weekly());
            var jobId = BackgroundJob.Enqueue<InitialDB>(x => x.createAdmin());
            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }


        public class InitialDB
        {
            private ApplicationDbContext db = new ApplicationDbContext();
            public void createAdmin()
            {
                if(db.Users.Where(u => u.Roles.FirstOrDefault().RoleId == "1").ToList().Count == 0)
                {
                    var store = new RoleStore<IdentityRole>(db);
                    var manager = new RoleManager<IdentityRole>(store);
                    var role = new IdentityRole();
                    role.Name = "Admin";


                    var userStore = new UserStore<ApplicationUser>(db);
                    var userManager = new UserManager<ApplicationUser>(userStore);
                    var admin = new ApplicationUser();
                    admin.UserName = "admin";
                    userManager.Create(admin, "admin");
                    userManager.AddToRole(admin.Id, "Admin");
                    db.SaveChanges();
                }
            }
        }

        public class EmailSender
        {
            private ApplicationDbContext db = new ApplicationDbContext();
            public void SendMail()
            {
                var senderEmail = new MailAddress("gradebookMVC@gmail.com", "Gradebook");

                var password = "gradebookMVC1!";
                var role = db.Roles.First(u => u.Name == "Parent");
                var students = db.Users.Where(u => u.Roles.FirstOrDefault().RoleId == "3").ToList();
                var subjects = db.Subjects.ToList();
                string message;
                var date = DateTime.Now.AddDays(-7);
                foreach (ApplicationUser u in students)
                {
                    message = "";
                    foreach (Subject s in subjects)
                    {
                        var grades = db.Grades.Where(i => i.StudentId == u.Id && i.SubjectId == s.SubjectId && i.Date > date).ToList();
                        if (grades.Count != 0)
                        {
                            message += "\n" + s.Name + ": ";
                            foreach (Grade g in grades)
                            {
                                message += g.Value + " ";
                            }
                        }

                    }

                    if (u.Parent != null && u.Parent.Email != null)
                    {
                        var receiverEmail = new MailAddress(u.Parent.Email, "Receiver");
                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(senderEmail.Address, password)
                        };
                        using (var mess = new MailMessage(senderEmail, receiverEmail)
                        {
                            Subject = "Zestawienie ocen z ostatniego tygodnia",
                            Body = message
                        })
                        {
                            smtp.Send(mess);
                        }
                    }
                }
            }

        }
    }
}
