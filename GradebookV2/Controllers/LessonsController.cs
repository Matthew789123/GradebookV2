using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GradebookV2.Models;
using Microsoft.AspNet.Identity;

namespace GradebookV2.Controllers
{
    public class LessonsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Teacher,Student")]
        public ActionResult getLessons(int classId, int subjectId)
        {
            if (User.IsInRole("Student"))
            {
                Class c = db.Classes.First(cl => cl.ClassId == classId);
                if (!c.Students.ToList().Contains(db.Users.First(s => s.Id == User.Identity.GetUserId())))
                    return RedirectToAction("Index", "News", null);
            }

            ViewBag.subject = db.Subjects.First(s => s.SubjectId == subjectId).Name;
            ViewBag.subjectId = subjectId;
            ViewBag.classId = classId;
            return View("Lessons", db.Lessons.Where(l => l.ClassId == classId && l.SubjectId == subjectId).OrderBy(l => l.Number).ToList());
        }
    }
}
