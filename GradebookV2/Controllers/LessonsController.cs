using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
                string id = User.Identity.GetUserId();
                if (!c.Students.ToList().Contains(db.Users.First(s => s.Id == id)))
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
            List<Tuple<Lesson, List<Models.File>>> list = new List<Tuple<Lesson, List<Models.File>>>();
            foreach (Lesson l in db.Lessons.Where(l => l.ClassId == classId && l.SubjectId == subjectId).OrderBy(l => l.Number).ToList())
            {
                list.Add(new Tuple<Lesson, List<Models.File>>(l, new List<Models.File>()));
                foreach (Models.File f in db.Files.Where(file => file.LessonId == l.LessonId))
                    list.Last().Item2.Add(f);
            }
            ViewBag.testsList = db.Tests.Where(test => test.ClassId == classId && test.SubjectId == subjectId).OrderByDescending(test => test.Start).ToList();
            ViewBag.count = ViewBag.testsList.Count;
            return View("Lessons", list);
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
        public ActionResult Create([Bind(Include = "Title,Content,SubjectId,ClassId")] Lesson lesson, HttpPostedFileBase[] files)
        {
            string id = User.Identity.GetUserId();
            if (db.SubjectClassTeacher.Where(sct => sct.ClassId == lesson.ClassId && sct.SubjectId == lesson.SubjectId && sct.TeacherId == id).ToList().Count == 0)
                return RedirectToAction("Index", "News", null);
            lesson.Number = db.Lessons.Where(l => l.SubjectId == lesson.SubjectId && l.ClassId == lesson.ClassId).ToList().Count();
            if (!ModelState.IsValid)
            {
                return View("Create", lesson);
            }
            lesson.Class = db.Classes.First(c => c.ClassId == lesson.ClassId);
            lesson.Subject = db.Subjects.First(s => s.SubjectId == lesson.SubjectId);
            List<Models.File> list = new List<Models.File>();
            foreach (HttpPostedFileBase file in files)
            {
                if (file == null)
                    continue;
                list.Add(new Models.File());
                MemoryStream target = new MemoryStream();
                file.InputStream.CopyTo(target);
                list.Last().Content = target.ToArray();
                list.Last().FileName = file.FileName;
            }
            db.Lessons.Add(lesson);
            db.SaveChanges();
            foreach (Models.File file in list)
            {
                file.LessonId = lesson.LessonId;
                file.Lesson = lesson;
                db.Files.Add(file);
            }
            db.SaveChanges();
            return RedirectToAction("getLessons", new { lesson.ClassId, lesson.SubjectId });
        }

        public FileResult downloadFile(int fileId)
        {
            Models.File file = db.Files.First(f => f.FileId == fileId);
            return File(file.Content, System.Net.Mime.MediaTypeNames.Application.Octet, file.FileName);
        }

        [Authorize(Roles = "Student")]
        public ActionResult checkTestDate(int testId)
        {
            Test test = db.Tests.First(t => t.TestId == testId);
            if ((DateTime.Now - test.Start).Value.TotalMinutes > 10 || DateTime.Now < test.Start)
                ViewBag.testError = "The test is now unavailable";
            return RedirectToAction("solveTest", "Tests", testId);
        }
    }
}
