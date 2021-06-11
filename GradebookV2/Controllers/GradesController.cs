using GradebookV2.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace GradebookV2.Controllers
{
    public class GradesController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Grades
        [Authorize(Roles = "Student")]
        public ActionResult MyGrades()
        {
            string userId = User.Identity.GetUserId();
            var subjects = db.Subjects.ToList();
            var grades = db.Grades.Where(g => g.StudentId == userId).ToList();
            var model = new MyGradesViewModel { Grades = grades, Subjects = subjects };
            return View("MyGrades",model);
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult deleteGrade(int gradeId)
        {
            Grade grade = db.Grades.First(g => g.GradeId == gradeId);
            int? classId = grade.Student.ClassId;
            db.Grades.Remove(grade);
            db.SaveChanges();
            return RedirectToAction("getGrades", "SubjectClassTeachers", new { subjectId = grade.SubjectId, classId = classId });
        }
    }
}