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
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: News
        public ActionResult Index()
        {
            return View(db.News.OrderByDescending(n => n.Date).ToList());
        }

        // GET: News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: News/Create
        [Authorize(Roles = "Admin,Teacher")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Teacher")]
        public ActionResult Create(string title, string content)
        {
            News news = new News();
            news.Title = title;
            news.Content = content;
            news.Date = DateTime.Now;
            news.TeacherId = User.Identity.GetUserId();
            string id = User.Identity.GetUserId();
            news.Author = db.Users.First(u => u.Id == id);
            db.News.Add(news);
            db.SaveChanges();
            return RedirectToAction("yourNews");
        }

        // GET: News/Edit/5
        [Authorize(Roles = "Admin,Teacher")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Teacher")]
        public ActionResult Edit([Bind(Include = "NewsId,Title,Content,Date,TeacherId")] News news)
        {
            if (ModelState.IsValid)
            {
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: News/Delete/5
        [Authorize(Roles = "Admin,Teacher")]
        public ActionResult Delete(int id)
        {
            db.News.Remove(db.News.First(n => n.NewsId == id));
            db.SaveChanges();
            return RedirectToAction("yourNews");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult yourNews()
        {
            string id = User.Identity.GetUserId();
            return View("YourNews", db.News.Where(n => n.TeacherId == id).OrderByDescending(n => n.Date).ToList());
        }
    }
}
