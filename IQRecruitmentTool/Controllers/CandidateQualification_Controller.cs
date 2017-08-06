using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IQRecruitmentTool.Models;
using System.Linq.Expressions;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace IQRecruitmentTool.Controllers
{
    public class CandidateQualification_Controller : Controller
    {
        private RecruitmentTestEntities db = new RecruitmentTestEntities();

		[Authorize(Roles = "Candidate (Job Seeker)")]
		// GET: CandidateQualification_
		public ActionResult Index()
        {
            return View(db.CandidateQualification_.ToList());
        }

		[Authorize(Roles = "Candidate (Job Seeker)")]
		// GET: CandidateQualification_/Details/5
		public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateQualification_ CandidateQualification_ = db.CandidateQualification_.Find(id);
            if (CandidateQualification_ == null)
            {
                return HttpNotFound();
            }
            return View(CandidateQualification_);
        }

		[Authorize(Roles = "Candidate (Job Seeker)")]
		// GET: CandidateQualification_/Create
		public ActionResult Create()            
        {
            ViewBag.QualType = new SelectList(db.ListHighestQualification,"HighestQualificationID","HighestQualification");
            return View();
        }

		[Authorize(Roles = "Candidate (Job Seeker)")]
		// POST: CandidateQualification_/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QualificationID,PID,QualificationType,Qualification,Institution,YearCompleted,MajorSubjects,ScoreAverage,YearStarted")] CandidateQualification_ CandidateQualification_)
        {
            ViewBag.QualType = new SelectList(db.ListHighestQualification, "HighestQualificationID", "HighestQualification");
            String UserID = User.Identity.GetUserId();
            CandidateQualification_.UserID = UserID;
            CandidateQualification_.UpdatedDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.CandidateQualification_.Add(CandidateQualification_);
                db.SaveChanges();
                return RedirectToAction("Index", "CandidatePersonalInfProfile"); 
            }

            return View(CandidateQualification_);
        }

		[Authorize(Roles = "Candidate (Job Seeker)")]
		// GET: CandidateQualification_/Edit/5
		public ActionResult Edit(int? QualificationID)
        {
            ViewBag.QualType = new SelectList(db.ListHighestQualification, "HighestQualificationID", "HighestQualification");
            if (QualificationID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateQualification_ CandidateQualification_ = db.CandidateQualification_.Find(QualificationID);
            if (CandidateQualification_ == null)
            {
                return RedirectToAction("Index", "CandidatePersonalInfProfile");
            }
            return View(CandidateQualification_);
        }

		[Authorize(Roles = "Candidate (Job Seeker)")]
		// POST: CandidateQualification_/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QualificationID,Qualification,Institution,QualificationType,Institution,YearCompleted,YearStarted,MajorSubjects,ScoreAverage")] CandidateQualification_ CandidateQualification_)
        {
            ViewBag.QualType = new SelectList(db.ListHighestQualification, "HighestQualificationID", "HighestQualification");
            String UserID = User.Identity.GetUserId();
            CandidateQualification_.UserID = UserID;
      
            CandidateQualification_.UpdatedDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(CandidateQualification_).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "CandidatePersonalInfProfile", new { area = "" });
            }
            return View(CandidateQualification_);
        }

		[Authorize(Roles = "Candidate (Job Seeker)")]
		// GET: CandidateQualification_/Delete/5
		public ActionResult Delete(int? QualificationID)
        {
            if (QualificationID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateQualification_ CandidateQualification_ = db.CandidateQualification_.Find(QualificationID);
            if (CandidateQualification_ == null)
            {
                return RedirectToAction("Index", "CandidatePersonalInfProfile");
            }
            return View(CandidateQualification_);
        }

		[Authorize(Roles = "Candidate (Job Seeker)")]
		// POST: CandidateQualification_/Delete/5
		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int QualificationID)
        {
            CandidateQualification_ CandidateQualification_ = db.CandidateQualification_.Find(QualificationID);
            db.CandidateQualification_.Remove(CandidateQualification_);
            db.SaveChanges();
            return RedirectToAction("Index", "CandidatePersonalInfProfile", new { area = "" });
          
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
