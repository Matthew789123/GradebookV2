using Hangfire;
using Microsoft.Owin;
using Owin;
using System;
using System.Net;
using System.Net.Mail;

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

            RecurringJob.AddOrUpdate(() => SendMail(),Cron.Weekly);

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }

        public void SendMail()
        {
            var senderEmail = new MailAddress("gradebookMVC@gmail.com", "Hangfire");
            var receiverEmail = new MailAddress("micgry21@gmail.com", "Receiver");
            var password = "gradebookMVC1!";
     
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
                Subject = "test",
                Body = "test"
            })
            {
                smtp.Send(mess);
            }
        }
    }
}
