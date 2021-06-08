using GradebookV2.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewMessage(string title, string message)
        {
            string teacherId = User.Identity.GetUserId();
            var teacher = db.Users.First(u => u.Id == teacherId);
            var @class = db.Classes.First(u => u.TeacherId == teacherId);
            List<ApplicationUser> Receivers = new List<ApplicationUser>();
            foreach (ApplicationUser s in @class.Students)
            {
                if (s.ParentId != null)
                {
                    var temp = db.Users.First(u => u.Id == s.ParentId);
                    Receivers.Add(temp);
                }
            }

            Message Message = new Message();
            Message.Content = message;
            Message.Title = title;
            Message.Date = DateTime.Now;
            db.Messages.Add(Message);


            foreach (ApplicationUser r in Receivers)
            {
                UserMessage UserMessage = new UserMessage();
                UserMessage.SenderId = teacherId;
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
            }

            db.SaveChanges();
            return RedirectToAction("MessageBox");
        }
    }
}