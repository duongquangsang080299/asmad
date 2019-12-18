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
    public class TraineeController : Controller
    {
        private tuananhEntities db = new tuananhEntities();

        // GET: Trainee
        public ActionResult Index()
        {
            var trainees = db.trainees.Include(t => t.user);
            return View(trainees.ToList());
        }

        // GET: Trainee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            trainee trainee = db.trainees.Find(id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            return View(trainee);
        }

        // GET: Trainee/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.users, "UserID", "Account");
            return View();
        }

        // POST: Trainee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TraineeID,UserID,TraineeName")] trainee trainee)
        {
            if (ModelState.IsValid)
            {
                db.trainees.Add(trainee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.users, "UserID", "Account", trainee.UserID);
            return View(trainee);
        }

        // GET: Trainee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            trainee trainee = db.trainees.Find(id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.users, "UserID", "Account", trainee.UserID);
            return View(trainee);
        }

        // POST: Trainee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TraineeID,UserID,TraineeName")] trainee trainee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.users, "UserID", "Account", trainee.UserID);
            return View(trainee);
        }

        // GET: Trainee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            trainee trainee = db.trainees.Find(id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            return View(trainee);
        }

        // POST: Trainee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            trainee trainee = db.trainees.Find(id);
            db.trainees.Remove(trainee);
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
