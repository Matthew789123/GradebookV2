using GradebookV2.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace GradebookV2.Controllers
{
    public class MessagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Messages
        public ActionResult MessageBox()
        {
            string userId = User.Identity.GetUserId();
            var receivedMessages = db.UserMessages.Where(u => u.ReceiverId == userId).ToList();
            var sendedMessages = db.UserMessages.Where(u => u.SenderId == userId).ToList();
            var model = new MessageBoxViewModel();
            model.ReceivedMessages = new List<Message>();
            model.SendedMessages = new List<Message>();
            foreach (UserMessage m in receivedMessages)
            {
                model.ReceivedMessages.Add(m.Message);
            }
            foreach (UserMessage m in sendedMessages)
            {
                model.SendedMessages.Add(m.Message);
            }
            return View(model);
        }
        [Authorize(Roles = "Teacher")]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public ActionResult NewMessage([Bind(Include = "Title,Content")] MessageViewModel message)
        {
            string userId = User.Identity.GetUserId();
            var teacher = db.Users.First(u => u.Id == userId);
            var @class = db.Classes.First(u => u.TeacherId == userId);
            List<ApplicationUser> Receivers = new List<ApplicationUser>();
            foreach (ApplicationUser s in @class.Students)
            {
                if (s.ParentId != null)
                {
                    var temp = db.Users.First(u => u.Id == s.ParentId);
                    Receivers.Add(temp);
                }
            }

            if(Receivers.Count != 0)
            {
                string sender = teacher.Name + " " + teacher.Surname;
                Message Message = new Message();
                Message.Content = message.Content;
                Message.Title = message.Title;
                Message.Date = DateTime.Now;
                db.Messages.Add(Message);
                foreach (ApplicationUser r in Receivers)
                {
                    UserMessage UserMessage = new UserMessage();
                    UserMessage.SenderId = userId;
                    UserMessage.Sender = teacher;
                    UserMessage.Message = Message;
                    UserMessage.ReceiverId = r.Id;
                    UserMessage.Receiver = r;
                    db.UserMessages.Add(UserMessage);
                    if (teacher.UserMessages == null)
                    {
                        teacher.UserMessages = new List<UserMessage>();
                    }
                    teacher.UserMessages.Add(UserMessage);

                    ///MAIL
                    if(r.Email != null)
                    {
                        var senderEmail = new MailAddress("gradebookMVC@gmail.com", sender);
                        var receiverEmail = new MailAddress(r.Email, "Receiver");
                        var password = "gradebookMVC1!";
                        var sub = message.Title;
                        var body = message.Content;
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
                            Subject = sub,
                            Body = body
                        })
                        {
                            smtp.Send(mess);
                        }
                    }
                }
                db.SaveChanges();
            }
            return RedirectToAction("MessageBox");
        }

        [Authorize(Roles = "Parent")]
        public ActionResult MessageToTeacher()
        {
            string parentId = User.Identity.GetUserId();
            var child = db.Users.First(u => u.ParentId == parentId);
            var classTeachers = db.SubjectClassTeacher.Where(u => u.ClassId == child.ClassId).ToList();
            var @class = db.Classes.First(u => u.ClassId == child.ClassId);
            List<Tuple<ApplicationUser, string>> teachers = new List<Tuple<ApplicationUser, string>>();
            if(classTeachers.Count != 0)
            {
                foreach (SubjectClassTeacher c in classTeachers)
                {
                    teachers.Add(new Tuple<ApplicationUser, string>(c.Teacher, c.Subject.Name));
                }
                teachers.Add(new Tuple<ApplicationUser, string>(@class.HomeroomTeacher, "Wychowawca"));
                ViewBag.Teachers = teachers;
                return View();
            }
            return RedirectToAction("MessageBox");
            
        }

        [HttpPost]
        [Authorize(Roles = "Parent")]
        public ActionResult MessageToTeacher([Bind(Include = "Title,Content")] MessageViewModel message, string teacherId)
        {
            if(!ModelState.IsValid)
            {
                return View(message);
            }

            string userId = User.Identity.GetUserId();
            var user = db.Users.First(u => u.Id == userId);
            var teacher = db.Users.First(u => u.Id == teacherId);
            Message Message = new Message();
            Message.Content = message.Content;
            Message.Title = message.Title;
            Message.Date = DateTime.Now;
            db.Messages.Add(Message);


            UserMessage UserMessage = new UserMessage();
            UserMessage.SenderId = userId;
            UserMessage.Sender = user;
            UserMessage.Message = Message;
            UserMessage.ReceiverId = teacherId;
            UserMessage.Receiver = teacher;

            db.UserMessages.Add(UserMessage);
            if (teacher.UserMessages == null)
            {
                teacher.UserMessages = new List<UserMessage>();
            }
            teacher.UserMessages.Add(UserMessage);

            db.SaveChanges();
            return RedirectToAction("MessageBox");
        }

    }
}