using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using GradebookV2.Models;

namespace GradebookV2.Controllers
{
    public class ClassesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Classes
        public ActionResult Index()
        {

            return View(db.Classes.ToList());
        }

        // GET: Classes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // GET: Classes/Create
        public ActionResult Create()
        {
            var teachers = db.Users.Where(u => u.Roles.FirstOrDefault().RoleId == "2").Where(u => u.ClassId == null);
            ViewBag.Teachers = teachers;
            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int grade, string name, string teacher)
        {
            Class c = new Class();
            c.Grade = grade;
            c.Name = name;
            c.TeacherId = teacher;
            var t = db.Users.First(u => u.Id == teacher);
            c.HomeroomTeacher = t;
            t.Class = c;
            t.ClassId = c.ClassId;
            db.Classes.Add(c);
            
            db.SaveChanges();
            return View("Index", db.Classes.ToList());
        }

        // GET: Classes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassId,Grade,Name,TeacherId")] Class @class)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@class).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@class);
        }

        // GET: Classes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Class @class = db.Classes.Find(id);
            if (@class == null)
            {
                return HttpNotFound();
            }
            return View(@class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Class @class = db.Classes.Find(id);
            db.Classes.Remove(@class);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult chooseTeacher()
        {
            ViewBag.Teachers = db.Users.Where(t => t.Roles.FirstOrDefault().RoleId == "2").ToList();
            ViewBag.Classes = db.Classes.ToList();
            ViewBag.Subjects = db.Subjects.ToList();
            return View("chooseTeacher");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult assignTeacher(int classId, string teacherId, int subjectId)
        {
            Class c = db.Classes.Single(cl => cl.ClassId == classId);
            ApplicationUser t =  db.Users.Single(te => te.Id == teacherId);
            Subject s = db.Subjects.Single(su => su.SubjectId == subjectId);
            SubjectClassTeacher sct = new SubjectClassTeacher();
            SubjectClassTeacher exists = db.SubjectClassTeacher.FirstOrDefault(u => u.ClassId == c.ClassId && u.SubjectId == s.SubjectId);
            if (exists != null)
            {
                ViewBag.ErrorMessage = "Klasa ma przypisanego nauczyciela do tego przedmiotu";
                return RedirectToAction("chooseTeacher");
            }
            sct.ClassId = classId;
            sct.Class = c;
            sct.TeacherId = teacherId;
            sct.Teacher = t;
            sct.SubjectId = subjectId;
            sct.Subject = s;
            db.SubjectClassTeacher.Add(sct);
            db.SaveChanges();
            return RedirectToAction("chooseTeacher");
        }
    }
}
