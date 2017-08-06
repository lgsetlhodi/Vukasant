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
    public class CompanyDescriptionsController : Controller
    {
        private RecruitmentTestEntities db = new RecruitmentTestEntities();

        // GET: CompanyDescriptions
     
        // GET: CompanyDescriptions/Create
     

        // GET: CompanyDescriptions/Edit/5
        public ActionResult Edit(int? DescriptionID, int CompanyID)
        {
            if (DescriptionID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyDescription companyDescription = db.CompanyDescription.Find(DescriptionID);
            if (companyDescription == null)
            {
                return HttpNotFound();
            }
            return View(companyDescription);
        }

        // POST: CompanyDescriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DescriptionID,CompanyID,Description")] CompanyDescription companyDescription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyDescription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "StorageCompany", new { area = "" });
            }
            return View(companyDescription);
        }

        // GET: CompanyDescriptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyDescription companyDescription = db.CompanyDescription.Find(id);
            if (companyDescription == null)
            {
                return HttpNotFound();
            }
            return View(companyDescription);
        }

        // POST: CompanyDescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CompanyDescription companyDescription = db.CompanyDescription.Find(id);
            db.CompanyDescription.Remove(companyDescription);
            db.SaveChanges();
            return RedirectToAction("Index", "StorageCompany", new { area = "" });
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
