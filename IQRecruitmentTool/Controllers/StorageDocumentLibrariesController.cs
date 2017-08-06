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
    public class StorageDocumentLibraryController : Controller
    {
        private RecruitmentTestEntities db = new RecruitmentTestEntities();

        // GET: StorageDocumentLibrary
        public ActionResult Index()
        {
            return View(db.StorageDocumentLibrary.ToList());
        }

        // GET: StorageDocumentLibrary/Details/5
        public ActionResult Details(int? RecordID)
        {
            if (RecordID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorageDocumentLibrary StorageDocumentLibrary = db.StorageDocumentLibrary.Find(RecordID);
            if (StorageDocumentLibrary == null)
            {
                return HttpNotFound();
            }
            return View(StorageDocumentLibrary);
        }

        // GET: StorageDocumentLibrary/Create
        public ActionResult Create()
        {
            ViewBag.Docutype = new SelectList(db.ListDocumentType, "DocumentTypeId", "DocumentType");

            return View();
        }

        // POST: StorageDocumentLibrary/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecordID,UserID,UserTypeID,DocumentTypeID,DocumentURL,Comments,DocName")] StorageDocumentLibrary StorageDocumentLibrary, HttpPostedFileBase file)
        {
            ViewBag.Docutype = new SelectList(db.ListDocumentType, "DocumentTypeId", "DocumentType");
            String UserID = User.Identity.GetUserId();
            StorageDocumentLibrary.UpdateDate = DateTime.Now;
            StorageDocumentLibrary.UserID = UserID;
            StorageDocumentLibrary.UserTypeID = 1;
            
            if (file.ContentLength > 0)
            {

                var fileName = Path.GetFileName(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                if (extension==".pdf" || extension == ".doc" || extension == ".docx")
                {
                var path = Path.Combine(Server.MapPath("~/UploadedDocuments/UploadedCV/"), UserID + StorageDocumentLibrary.DocumentTypeID + "_3" + extension);
                file.SaveAs(path);
                ViewBag.Message = "You have not specified a file.";
                StorageDocumentLibrary.DocumentURL =  UserID + StorageDocumentLibrary.DocumentTypeID + "_3" + extension;
                    if (ModelState.IsValid)
                    {
                        db.StorageDocumentLibrary.Add(StorageDocumentLibrary);
                        db.SaveChanges();
                        return RedirectToAction("Index", "CandidatePersonalInfProfile");
                    }
                }
                else
                {
                    ViewBag.Docutype = new SelectList(db.ListDocumentType, "DocumentTypeId", "DocumentType");
                    ViewBag.FileFormat = "Please note that we only accept word documents and pdf formatted files only";
                  
                }
            }
           

            return View(StorageDocumentLibrary);
        }

        // GET: StorageDocumentLibrary/Edit/5
        public ActionResult Edit(int? RecordID)
        {
            ViewBag.Docutype = new SelectList(db.ListDocumentType, "DocumentTypeId", "DocumentType");
            if (RecordID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorageDocumentLibrary StorageDocumentLibrary = db.StorageDocumentLibrary.Find(RecordID);
            if (StorageDocumentLibrary == null)
            {
                return HttpNotFound();
            }
            return View(StorageDocumentLibrary);
        }

        // POST: StorageDocumentLibrary/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecordID,UserID,UserTypeID,DocName,DocumentTypeID,DocumentURL,Comments")] StorageDocumentLibrary StorageDocumentLibrary)
        {
            ViewBag.Docutype = new SelectList(db.ListDocumentType, "DocumentTypeId", "DocumentType");
            String UserID = User.Identity.GetUserId();
            StorageDocumentLibrary.UpdateDate = DateTime.Now;
            StorageDocumentLibrary.UserID = UserID;
            if (ModelState.IsValid)
            {
                db.Entry(StorageDocumentLibrary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "CandidatePersonalInfProfile");
            }
            return View(StorageDocumentLibrary);
        }

        // GET: StorageDocumentLibrary/Delete/5
        public ActionResult Delete(int? RecordID)
        {
            if (RecordID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorageDocumentLibrary StorageDocumentLibrary = db.StorageDocumentLibrary.Find(RecordID);
            if (StorageDocumentLibrary == null)
            {
                return HttpNotFound();
            }
            return View(StorageDocumentLibrary);
        }

        // POST: StorageDocumentLibrary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int RecordID)
        {
            StorageDocumentLibrary StorageDocumentLibrary = db.StorageDocumentLibrary.Find(RecordID);
            db.StorageDocumentLibrary.Remove(StorageDocumentLibrary);
            db.SaveChanges();
            return RedirectToAction("Index", "CandidatePersonalInfProfile");
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
