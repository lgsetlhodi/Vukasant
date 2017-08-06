using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IQRecruitmentTool.Models;

namespace IQRecruitmentTool.Controllers
{
    public class ListRaceController : Controller
    {
        private RecruitmentTestEntities db = new RecruitmentTestEntities();

        // GET: ListRace
        public ActionResult Index()
        {
            return View(db.ListRace.ToList());
        }

        // GET: ListRace/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListRace ListRace = db.ListRace.Find(id);
            if (ListRace == null)
            {
                return HttpNotFound();
            }
            return View(ListRace);
        }

        // GET: ListRace/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ListRace/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RaceID,Race")] ListRace ListRace)
        {
            if (ModelState.IsValid)
            {
                db.ListRace.Add(ListRace);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ListRace);
        }

        // GET: ListRace/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListRace ListRace = db.ListRace.Find(id);
            if (ListRace == null)
            {
                return HttpNotFound();
            }
            return View(ListRace);
        }

        // POST: ListRace/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RaceID,Race")] ListRace ListRace)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ListRace).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ListRace);
        }

        // GET: ListRace/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListRace ListRace = db.ListRace.Find(id);
            if (ListRace == null)
            {
                return HttpNotFound();
            }
            return View(ListRace);
        }

        // POST: ListRace/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ListRace ListRace = db.ListRace.Find(id);
            db.ListRace.Remove(ListRace);
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
