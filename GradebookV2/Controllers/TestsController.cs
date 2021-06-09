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
    public class TestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Teacher")]
        public ActionResult createTest(int classId, int subjectId)
        {
            string teacherId = User.Identity.GetUserId();
            Test test = new Test();
            test.SubjectId = subjectId;
            test.ClassId = classId;
            return View("Create", test);
        }

        [Authorize(Roles = "Teacher")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create([Bind(Include = "SubjectId,ClassId,Title,Start,Duration")] Test test, string questions)
        {
            if (questions == "")
            {
                ViewBag.errorMessage = "You need to add atleast one question";
                return View();
            }
            test.Subject = db.Subjects.First(s => s.SubjectId == test.SubjectId);
            test.Class = db.Classes.First(c => c.ClassId == test.ClassId); 
            db.Tests.Add(test);
            string[] split = questions.Split('"');
            int index = 5;
            while (index < split.Length)
            {
                Question question = new Question();
                question.Content = split[index];
                index += 4;
                question.AnswerA = split[index];
                index += 4;
                question.AnswerB = split[index];
                index += 4;
                question.AnswerC = split[index];
                index += 4;
                question.AnswerD = split[index];
                index += 4;
                question.CorrectAnswer = split[index];
                index += 4;
                question.Points = Convert.ToInt32(split[index]);
                index += 3;
                question.Number = Convert.ToInt32(new String (split[index].Where(Char.IsDigit).ToArray()));
                index += 3;
                question.TestId = test.TestId;
                question.Test = test;
                if (test.Questions == null)
                    test.Questions = new List<Question>();
                test.Questions.Add(question);
                db.Questions.Add(question);
            }
            db.SaveChanges();
            return RedirectToAction("getLessons", "Lessons", new { test.ClassId, test.SubjectId });
        }

        [Authorize(Roles = "Student")]
        public ActionResult solveTest(int testId)
        {
            Test test = db.Tests.First(t => t.TestId == testId);
            string id = User.Identity.GetUserId();
            ApplicationUser student = db.Users.First(u => u.Id == id);
            if (!test.Class.Students.Contains(student) || test.Students.Contains(student))
                return RedirectToAction("Index", "News", null);

            test.Questions.OrderBy(q => q.Number);
            return View("SolveTest", test);
        }

        [Authorize(Roles = "Student")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult testDone(int testId, string answers)
        {
            Test test = db.Tests.First(t => t.TestId == testId);
            List<Question> questions = test.Questions.OrderBy(q => q.Number).ToList();
            int total = 0, points = 0;
            foreach (Question q in questions)
                total += q.Points;
            string[] a = answers.Split(',');
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == questions[i].CorrectAnswer)
                    points += questions[i].Points;
            }

            Grade grade = new Grade();
            decimal result = Decimal.Round((points / total) * 100, 2);
            if (result > 90)
                grade.Value = 6;
            else if (result == 90)
                grade.Value = 5.75M;
            else if (result > 89)
                grade.Value = 5.5M;
            else if (result > 80)
                grade.Value = 5;
            else if (result == 80)
                grade.Value = 4.75M;
            else if (result > 79)
                grade.Value = 4.5M;
            else if (result > 70)
                grade.Value = 4;
            else if (result == 70)
                grade.Value = 3.75M;
            else if (result > 69)
                grade.Value = 3.5M;
            else if (result > 60)
                grade.Value = 3;
            else if (result == 50)
                grade.Value = 2.75M;
            else if (result > 49)
                grade.Value = 2.5M;
            else if (result > 40)
                grade.Value = 2;
            else
                grade.Value = 1;
            grade.Date = DateTime.Now;
            grade.Type = "Test";
            grade.Comment = test.Title;
            grade.SubjectId = test.SubjectId;
            grade.Subject = test.Subject;
            grade.StudentId = User.Identity.GetUserId();
            grade.Student = db.Users.First(u => u.Id == grade.StudentId);
            db.Grades.Add(grade);
            if (test.Students == null)
                test.Students = new List<ApplicationUser>();
            test.Students.Add(grade.Student);
            db.SaveChanges();

            ViewBag.Total = total;
            ViewBag.Points = points;
            return View("TestDone");
        }
    }
}
