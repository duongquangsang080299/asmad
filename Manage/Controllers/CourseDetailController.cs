using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Manage.Models;

namespace Manage.Controllers
{
    [Authorize(Roles = "Training Staff")]
    public class CourseDetailController : Controller
    {
        private tuananhEntities db = new tuananhEntities();

        // GET: CourseDetail
        public ActionResult Index()
        {
            var coursedetails = db.coursedetails.Include(c => c.Course).Include(c => c.topic);
            return View(coursedetails.ToList());
        }

        // GET: CourseDetail/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            coursedetail coursedetail = db.coursedetails.Find(id);
            if (coursedetail == null)
            {
                return HttpNotFound();
            }
            return View(coursedetail);
        }

        // GET: CourseDetail/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            ViewBag.TopicID = new SelectList(db.topics, "TopicID", "TopicDescription");
            return View();
        }

        // POST: CourseDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseDetail1,TopicID,CourseID")] coursedetail coursedetail)
        {
            if (ModelState.IsValid)
            {
                db.coursedetails.Add(coursedetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", coursedetail.CourseID);
            ViewBag.TopicID = new SelectList(db.topics, "TopicID", "TopicDescription", coursedetail.TopicID);
            return View(coursedetail);
        }

        // GET: CourseDetail/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            coursedetail coursedetail = db.coursedetails.Find(id);
            if (coursedetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", coursedetail.CourseID);
            ViewBag.TopicID = new SelectList(db.topics, "TopicID", "TopicDescription", coursedetail.TopicID);
            return View(coursedetail);
        }

        // POST: CourseDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseDetail1,TopicID,CourseID")] coursedetail coursedetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coursedetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", coursedetail.CourseID);
            ViewBag.TopicID = new SelectList(db.topics, "TopicID", "TopicDescription", coursedetail.TopicID);
            return View(coursedetail);
        }

        // GET: CourseDetail/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            coursedetail coursedetail = db.coursedetails.Find(id);
            if (coursedetail == null)
            {
                return HttpNotFound();
            }
            return View(coursedetail);
        }

        // POST: CourseDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            coursedetail coursedetail = db.coursedetails.Find(id);
            db.coursedetails.Remove(coursedetail);
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
    }
}
