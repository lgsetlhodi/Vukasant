using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IQRecruitmentTool.Models;
using System.IO;
using System.Web.Security;
using Microsoft.AspNet.Identity;


namespace IQRecruitmentTool.Controllers
{
    public class AvatarCompanyController : Controller
    {
        private RecruitmentTestEntities db = new RecruitmentTestEntities();

        // GET: AvatarCompany
        public ActionResult Index()
        {
            return View(db.StorageCompany.ToList());
        }

        // GET: AvatarCompany/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorageCompany storageCompany = db.StorageCompany.Find(id);
            if (storageCompany == null)
            {
                return HttpNotFound();
            }
            return View(storageCompany);
        }

        // GET: AvatarCompany/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AvatarCompany/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyID,CreatedBy,MobileNumber,CompanyName,CompanyRegistrationNumber,CompanyTelephoneNumber,CompanyEmail,TownID,CompanyAddress,CompanySize,YearsInBusiness,BusinessSector,CreateDate,UpdateDate,UpdatedBy,BeeLevel,imgUrl")] StorageCompany storageCompany)
        {
            if (ModelState.IsValid)
            {
                db.StorageCompany.Add(storageCompany);
                db.SaveChanges();
                return RedirectToAction("Index", "StorageCompany");
            }

            return View(storageCompany);
        }

        // GET: AvatarCompany/Edit/5
        public ActionResult Edit(int? CompanyID)
        {

            if (CompanyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            StorageCompany storageCompany = db.StorageCompany.Find(CompanyID);
            if (storageCompany == null)
            {
                return HttpNotFound();
            }
            return View(storageCompany);
        }

        // POST: AvatarCompany/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyID,imgUrl")] StorageCompany storageCompany, HttpPostedFileBase file)
        {
            StorageCompany sstorageCompany = db.StorageCompany.Find(storageCompany.CompanyID);
            ViewBag.FileFormat = "";
            if (ModelState.IsValid)
            {
                String UserID = User.Identity.GetUserId();

                if (file.ContentLength > 0)
                {
                   

                     var fileName = Path.GetFileName(file.FileName);
                    var extension = Path.GetExtension(file.FileName);
                    if (extension == ".png" || extension == ".jpg" || extension == ".gif")
                    {
                        var path = Path.Combine(Server.MapPath("~/Avatars/Company/"), UserID + storageCompany.imgUrl + "_img" + extension);
                        file.SaveAs(path);
                        ViewBag.Message = "You have not specified an Image.";
                        sstorageCompany.imgUrl = UserID + storageCompany.imgUrl + "_img" + extension;
                        if (ModelState.IsValid)
                        {
                            db.Entry(sstorageCompany).Property(x => x.imgUrl).IsModified = true;
                            db.SaveChanges();
                            return RedirectToAction("Index", "StorageCompany");
                        }
                    }
                    else
                    {
                        ViewBag.FileFormat = "Please note that we only accept jpg,gif and png images";

                    }
                }
           
                return RedirectToAction("Index", "StorageCompany");

            }
        
            return View(storageCompany);
        }

        // GET: AvatarCompany/Delete/5
        public ActionResult Delete(int? CompanyID)
        {
            if (CompanyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorageCompany storageCompany = db.StorageCompany.Find(CompanyID);
            if (storageCompany == null)
            {
                return HttpNotFound();
            }
            return View(storageCompany);
        }

        // POST: AvatarCompany/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int CompanyID)
        {
            StorageCompany storageCompany = db.StorageCompany.Find(CompanyID);
            db.StorageCompany.Remove(storageCompany);
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
