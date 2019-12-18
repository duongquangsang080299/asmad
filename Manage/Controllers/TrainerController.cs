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
    public class TrainerController : Controller
    {
        private tuananhEntities db = new tuananhEntities();

        // GET: Trainer
        public ActionResult Index()
        {
            var trainers = db.trainers.Include(t => t.user);
            return View(trainers.ToList());
        }

        // GET: Trainer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            trainer trainer = db.trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // GET: Trainer/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.users, "UserID", "Account");
            return View();
        }

        // POST: Trainer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrainerID,UserID,TrainerName")] trainer trainer)
        {
            if (ModelState.IsValid)
            {
                db.trainers.Add(trainer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.users, "UserID", "Account", trainer.UserID);
            return View(trainer);
        }

        // GET: Trainer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            trainer trainer = db.trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.users, "UserID", "Account", trainer.UserID);
            return View(trainer);
        }

        // POST: Trainer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrainerID,UserID,TrainerName")] trainer trainer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.users, "UserID", "Account", trainer.UserID);
            return View(trainer);
        }

        // GET: Trainer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            trainer trainer = db.trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // POST: Trainer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            trainer trainer = db.trainers.Find(id);
            db.trainers.Remove(trainer);
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
