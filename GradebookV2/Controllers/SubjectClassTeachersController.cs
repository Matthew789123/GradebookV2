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
    public class SubjectClassTeachersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Teacher")]
        public ActionResult yourClasses()
        {
            string id = User.Identity.GetUserId();
            return View("YourClasses", db.SubjectClassTeacher.Where(sct => sct.TeacherId == id));
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult getGrades(int classId, int subjectId)
        {
            string id = User.Identity.GetUserId();
            if (db.SubjectClassTeacher.Where(sct => sct.ClassId == classId && sct.SubjectId == subjectId && sct.TeacherId == id).ToList().Count == 0)
                return RedirectToAction("Index", "News", null);

            List<Tuple<ApplicationUser, List<Grade>>> list = new List<Tuple<ApplicationUser, List<Grade>>>();
            foreach (ApplicationUser student in db.Users.Where(s => s.Roles.FirstOrDefault().RoleId == "3" && s.ClassId == classId).OrderBy(s => s.Surname).ThenBy(s => s.Name))
            {
                list.Add(new Tuple<ApplicationUser, List<Grade>>(student, new List<Grade>()));
                foreach (Grade grade in db.Grades.Where(g => g.StudentId == student.Id && g.SubjectId == subjectId))
                    list.Last().Item2.Add(grade);
            }
            ViewBag.subjectId = subjectId;
            return View("getGrades", list);
        }

        [Authorize(Roles = "Teacher")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult addGrade(string studentId, int subjectId, decimal value, string type, string comment)
        {
            Grade grade = new Grade();
            grade.Value = value;
            switch(grade.Value)
            {
                case 6M:
                    grade.Name = "6";
                    break;
                case 5.75M:
                    grade.Name = "6-";
                    break;
                case 5.5M:
                    grade.Name = "5+";
                    break;
                case 5M:
                    grade.Name = "5";
                    break;
                case 4.75M:
                    grade.Name = "5-";
                    break;
                case 4.5M:
                    grade.Name = "4+";
                    break;
                case 4M:
                    grade.Name = "4";
                    break;
                case 3.75M:
                    grade.Name = "4-";
                    break;
                case 3.5M:
                    grade.Name = "3+";
                    break;
                case 3M:
                    grade.Name = "3";
                    break;
                case 2.75M:
                    grade.Name = "2+";
                    break;
                case 2.5M:
                    grade.Name = "2";
                    break;
                case 1.75M:
                    grade.Name = "2-";
                    break;
                case 1M:
                    grade.Name = "1";
                    break;

            }
            grade.Type = type;
            grade.Comment = comment;
            grade.SubjectId = subjectId;
            grade.Subject = db.Subjects.First(s => s.SubjectId == subjectId);
            grade.StudentId = studentId;
            ApplicationUser student = db.Users.First(s => s.Id == studentId);
            grade.Student = student;
            grade.Date = DateTime.Now;
            db.Grades.Add(grade);
            db.SaveChanges();
            return RedirectToAction("getGrades", new { db.Classes.First(c => c.ClassId == student.ClassId).ClassId, subjectId});
        }
    }
}
