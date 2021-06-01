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
            SubjectClassTeacher sct = db.SubjectClassTeacher.First(s => s.ClassId == classId && s.SubjectId == subjectId && s.TeacherId == teacherId);
            Test test = new Test();
            test.SubjectClassTeacherId = sct.SubjectClassTeacherId;
            return View("Create", test);
        }

        [Authorize(Roles = "Teacher")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create([Bind(Include = "SubjectClassTeacherId,Title,Start,Duration")] Test test, string questions)
        {
            if (questions == "")
            {
                ViewBag.errorMessage = "You need to add atleast one question";
                return View();
            }
            test.SubjectClassTeacher = db.SubjectClassTeacher.First(sct => sct.SubjectClassTeacherId == test.SubjectClassTeacherId);
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
                index += 6;
                question.TestId = test.TestId;
                question.Test = test;
                db.Questions.Add(question);
            }
            db.SaveChanges();
            return RedirectToAction("getLessons", "Lessons", new { test.SubjectClassTeacher.ClassId, test.SubjectClassTeacher.SubjectId });
        }
    }
}
