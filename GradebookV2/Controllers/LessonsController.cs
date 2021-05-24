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
            else
            {
                string id = User.Identity.GetUserId();
                if (db.SubjectClassTeacher.Where(sct => sct.ClassId == classId && sct.SubjectId == subjectId && sct.TeacherId == id).ToList().Count == 0)
                    return RedirectToAction("Index", "News", null);
            }

            ViewBag.subject = db.Subjects.First(s => s.SubjectId == subjectId).Name;
            ViewBag.subjectId = subjectId;
            ViewBag.classId = classId;
            return View("Lessons", db.Lessons.Where(l => l.ClassId == classId && l.SubjectId == subjectId).OrderByDescending(l => l.Number).ToList());
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult createLesson(int classId, int subjectId)
        {
            Lesson lesson = new Lesson();
            lesson.ClassId = classId;
            lesson.SubjectId = subjectId;
            return View("Create", lesson);
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Content,Files,SubjectId,ClassId")] Lesson lesson)
        {
            string id = User.Identity.GetUserId();
            if (db.SubjectClassTeacher.Where(sct => sct.ClassId == lesson.ClassId && sct.SubjectId == lesson.SubjectId && sct.TeacherId == id).ToList().Count == 0)
                return RedirectToAction("Index", "News", null);
            lesson.Number = db.Lessons.Where(l => l.SubjectId == lesson.SubjectId && l.ClassId == lesson.ClassId).ToList().Count();
            lesson.Class = db.Classes.First(c => c.ClassId == lesson.ClassId);
            lesson.Subject = db.Subjects.First(s => s.SubjectId == lesson.SubjectId);
            if (ModelState.IsValid)
            {
                db.Lessons.Add(lesson);
                db.SaveChanges();
            }
            return RedirectToAction("getLessons", new { lesson.ClassId, lesson.SubjectId });
        }
    }
}
