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
    }
}
