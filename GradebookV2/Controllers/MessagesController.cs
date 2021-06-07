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

    }
}