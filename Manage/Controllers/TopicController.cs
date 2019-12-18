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
    public class TopicController : Controller
    {
        private tuananhEntities db = new tuananhEntities();

        // GET: Topic
        public ActionResult Index()
        {
            var topics = db.topics.Include(t => t.trainer);
            return View(topics.ToList());
        }

        // GET: Topic/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            topic topic = db.topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: Topic/Create
        public ActionResult Create()
        {
            ViewBag.TrainerID = new SelectList(db.trainers, "TrainerID", "TrainerName");
            return View();
        }

        // POST: Topic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TopicID,TrainerID,TopicDescription,TopicName")] topic topic)
        {
            if (ModelState.IsValid)
            {
                db.topics.Add(topic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TrainerID = new SelectList(db.trainers, "TrainerID", "TrainerName", topic.TrainerID);
            return View(topic);
        }

        // GET: Topic/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            topic topic = db.topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            ViewBag.TrainerID = new SelectList(db.trainers, "TrainerID", "TrainerName", topic.TrainerID);
            return View(topic);
        }

        // POST: Topic/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TopicID,TrainerID,TopicDescription,TopicName")] topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TrainerID = new SelectList(db.trainers, "TrainerID", "TrainerName", topic.TrainerID);
            return View(topic);
        }

        // GET: Topic/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            topic topic = db.topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Topic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            topic topic = db.topics.Find(id);
            db.topics.Remove(topic);
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
