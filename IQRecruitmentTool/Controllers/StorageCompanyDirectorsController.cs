using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IQRecruitmentTool.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace IQRecruitmentTool.Controllers
{
    public class StorageCompanyDirectorsController : Controller
    {
        private RecruitmentTestEntities db = new RecruitmentTestEntities();

        // GET: StorageCompanyDirectors
        public ActionResult Index()
        {
            return View(db.StorageCompanyDirectors.ToList());
        }

        // GET: StorageCompanyDirectors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorageCompanyDirectors StorageCompanyDirector = db.StorageCompanyDirectors.Find(id);
            if (StorageCompanyDirector == null)
            {
                return HttpNotFound();
            }
            return View(StorageCompanyDirector);
        }

        // GET: StorageCompanyDirectors/Create
        public ActionResult Create(int CompanyID)
        {
            ViewBag.Datapass = CompanyID;
            return View();
        }

        // POST: StorageCompanyDirectors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DirectorID,UserID,FirstName,Surname,MobileNumber,EmailAddress,CompanyID,RegistrationStatusID")] StorageCompanyDirectors StorageCompanyDirectors)
        {
            StorageCompanyDirectors.UserID = User.Identity.GetUserId();
            //StorageCompanyDirector.CompanyID = ;
            StorageCompanyDirectors.RegistrationStatusID = 0;
            if (ModelState.IsValid)
            {
                db.StorageCompanyDirectors.Add(StorageCompanyDirectors);
                db.SaveChanges();

                return RedirectToAction("Index", "StorageCompany");
            }

            return View(StorageCompanyDirectors);
        }

        // GET: StorageCompanyDirectors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorageCompanyDirectors StorageCompanyDirector = db.StorageCompanyDirectors.Find(id);
            if (StorageCompanyDirector == null)
            {
                return HttpNotFound();
            }
            return View(StorageCompanyDirector);
        }

        // POST: StorageCompanyDirectors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DirectorID,UserID,FirstName,Surname,MobileNumber,EmailAddress,RegistrationStatusID")] StorageCompanyDirectors StorageCompanyDirector)
        {
            if (ModelState.IsValid)
            {
                db.Entry(StorageCompanyDirector).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(StorageCompanyDirector);
        }

        // GET: StorageCompanyDirectors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorageCompanyDirectors StorageCompanyDirector = db.StorageCompanyDirectors.Find(id);
            if (StorageCompanyDirector == null)
            {
                return HttpNotFound();
            }
            return View(StorageCompanyDirector);
        }

        // POST: StorageCompanyDirectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StorageCompanyDirectors StorageCompanyDirector = db.StorageCompanyDirectors.Find(id);
            db.StorageCompanyDirectors.Remove(StorageCompanyDirector);
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
