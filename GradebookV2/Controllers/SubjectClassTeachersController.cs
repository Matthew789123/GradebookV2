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
            List<Tuple<ApplicationUser, List<Grade>>> list = new List<Tuple<ApplicationUser, List<Grade>>>();
            foreach (ApplicationUser student in db.Users.Where(s => s.Roles.FirstOrDefault().RoleId == "3" && s.ClassId == classId).OrderBy(s => s.Surname).ThenBy(s => s.Name))
            {
                list.Add(new Tuple<ApplicationUser, List<Grade>>(student, new List<Grade>()));
                foreach (Grade grade in db.Grades.Where(g => g.StudentId == student.Id && g.SubjectId == subjectId))
                    list.Last().Item2.Add(grade);
            }
            return View("getGrades", list);
        }
    }
}
