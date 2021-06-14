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
                if(db.Roles.ToList().Count == 0)
                {
                    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                    // first we create Admin rool    

                    
                        var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                        role.Name = "Admin";
                        role.Id = "1";
                        try
                        {
                            roleManager.Create(role);
                        }
                        catch { }


                        var user = new ApplicationUser();
                        user.UserName = "admin";
                        string userPWD = "admin1";
                        var chkUser = UserManager.Create(user, userPWD);
                        //Add default User to Role Admin    
                        if (chkUser.Succeeded)
                        {
                            var result1 = UserManager.AddToRole(user.Id, "Admin");

                        }
                    

                    var parent = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                    parent.Name = "Parent";
                    parent.Id = "4";
                    try
                    {
                        roleManager.Create(parent);
                    }
                    catch { }

                    var teacher = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                    teacher.Name = "Teacher";
                    teacher.Id = "2";
                    try
                    {
                        roleManager.Create(teacher);
                    }
                    catch { }

                    var student = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                    student.Name = "Student";
                    student.Id = "3";
                    try
                    {
                        roleManager.Create(student);
                    }
                    catch { }
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
