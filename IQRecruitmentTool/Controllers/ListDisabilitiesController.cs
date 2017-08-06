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
    public class ListDisabilityController : Controller
    {
        private RecruitmentTestEntities db = new RecruitmentTestEntities();

        // GET: ListDisability
        public ActionResult Index()
        {
            return View(db.ListDisability.ToList());
        }

        // GET: ListDisability/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListDisability ListDisability = db.ListDisability.Find(id);
            if (ListDisability == null)
            {
                return HttpNotFound();
            }
            return View(ListDisability);
        }

        // GET: ListDisability/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }

        // POST: ListDisability/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DisabilityID,Disability")] ListDisability ListDisability)
        {
            if (ModelState.IsValid)
            {
                db.ListDisability.Add(ListDisability);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ListDisability);
        }

        // GET: ListDisability/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListDisability ListDisability = db.ListDisability.Find(id);
            if (ListDisability == null)
            {
                return HttpNotFound();
            }
            return View(ListDisability);
        }

        // POST: ListDisability/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DisabilityID,Disability")] ListDisability ListDisability)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ListDisability).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ListDisability);
        }

        // GET: ListDisability/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListDisability ListDisability = db.ListDisability.Find(id);
            if (ListDisability == null)
            {
                return HttpNotFound();
            }
            return View(ListDisability);
        }

        // POST: ListDisability/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ListDisability ListDisability = db.ListDisability.Find(id);
            db.ListDisability.Remove(ListDisability);
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
