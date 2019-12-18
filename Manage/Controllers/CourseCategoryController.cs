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
    public class CourseCategoryController : Controller
    {
        private tuananhEntities db = new tuananhEntities();

        // GET: CourseCategory
        public ActionResult Index()
        {
            return View(db.coursecategories.ToList());
        }

        // GET: CourseCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            coursecategory coursecategory = db.coursecategories.Find(id);
            if (coursecategory == null)
            {
                return HttpNotFound();
            }
            return View(coursecategory);
        }

        // GET: CourseCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseCategoryID,CourseCategoryName,CourseCategoryDescription")] coursecategory coursecategory)
        {
            if (ModelState.IsValid)
            {
                db.coursecategories.Add(coursecategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(coursecategory);
        }

        // GET: CourseCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            coursecategory coursecategory = db.coursecategories.Find(id);
            if (coursecategory == null)
            {
                return HttpNotFound();
            }
            return View(coursecategory);
        }

        // POST: CourseCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseCategoryID,CourseCategoryName,CourseCategoryDescription")] coursecategory coursecategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coursecategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(coursecategory);
        }

        // GET: CourseCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            coursecategory coursecategory = db.coursecategories.Find(id);
            if (coursecategory == null)
            {
                return HttpNotFound();
            }
            return View(coursecategory);
        }

        // POST: CourseCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            coursecategory coursecategory = db.coursecategories.Find(id);
            db.coursecategories.Remove(coursecategory);
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
