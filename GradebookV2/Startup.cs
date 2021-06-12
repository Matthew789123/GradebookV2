using GradebookV2.Models;
using Hangfire;
using Microsoft.Owin;
using Owin;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;

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

            RecurringJob.AddOrUpdate<EmailSender>(x => x.SendMail(), Cron.Weekly());

            app.UseHangfireDashboard();
            app.UseHangfireServer();

        }

        public class EmailSender
        {
            public ApplicationDbContext db = new ApplicationDbContext();
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
                    foreach(Subject s in subjects)
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
