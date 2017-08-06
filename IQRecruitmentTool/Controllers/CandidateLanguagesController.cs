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
    public class CandidateLanguageController : Controller
    {
        private RecruitmentTestEntities db = new RecruitmentTestEntities();

		[Authorize(Roles = "Candidate (Job Seeker)")]
		// GET: CandidateLanguage
		public ActionResult Index()
        {
            return View(db.CandidateLanguage.ToList());
        }

		[Authorize(Roles = "Candidate (Job Seeker)")]
		// GET: CandidateLanguage/Details/5
		public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateLanguage CandidateLanguage = db.CandidateLanguage.Find(id);
            if (CandidateLanguage == null)
            {
                return RedirectToAction("Index", "CandidatePersonalInfProfile");
            }
            return View(CandidateLanguage);
        }

		[Authorize(Roles = "Candidate (Job Seeker)")]
		// GET: CandidateLanguage/Create
		public ActionResult Create()
        {
            ViewBag.Language = new SelectList(db.ListLanguage, "LanguageID", "Language");
            ViewBag.Decisionst = new SelectList(db.Decision, "IntDecision", "Decision1");
            return View();
        }

		[Authorize(Roles = "Candidate (Job Seeker)")]
		// POST: CandidateLanguage/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CandidateLanguageID,UserID,Language,Read,Write,Speak")] CandidateLanguage CandidateLanguage)
        {

            String UserID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                if (db.CandidateLanguage.Any(p => p.UserID == UserID & p.Language==CandidateLanguage.Language))
                {
                    ModelState.AddModelError("Language", "You have already saved this language.");
                    ViewBag.Language = new SelectList(db.ListLanguage, "LanguageID", "Language");
                    ViewBag.Decisionst = new SelectList(db.Decision, "IntDecision", "Decision1");
                }
                else
                {
                    CandidateLanguage.UpdateDate = DateTime.Now;
                CandidateLanguage.UserID = User.Identity.GetUserId();
                db.CandidateLanguage.Add(CandidateLanguage);
                db.SaveChanges();
                    return RedirectToAction("Index", "CandidatePersonalInfProfile");
                }
            }

            return View(CandidateLanguage);
        }

		[Authorize(Roles = "Candidate (Job Seeker)")]
		// GET: CandidateLanguage/Edit/5
		public ActionResult Edit(int? CandidateLanguageID, string LanguageType, int? LanguageID)
        {
            ViewBag.LngID = LanguageID;
            ViewBag.Language = LanguageType;
            ViewBag.Decisionst = new SelectList(db.Decision, "IntDecision", "Decision1");
            if (CandidateLanguageID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateLanguage CandidateLanguage = db.CandidateLanguage.Find(CandidateLanguageID);
            //CandidateLanguage.UpdateDate = DateTime.Now;
            if (CandidateLanguage == null)
            {
                return RedirectToAction("Index","CandidatePersonalInfProfile");
            }
            
            return View(CandidateLanguage);
        }

		[Authorize(Roles = "Candidate (Job Seeker)")]
		// POST: CandidateLanguage/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CandidateLanguageID,UserID,Language,Read,Write,Speak")] CandidateLanguage CandidateLanguage)
        {
           

         
            CandidateLanguage.UpdateDate = DateTime.Now;
            CandidateLanguage.UserID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.Entry(CandidateLanguage).State = EntityState.Modified;
              
                db.SaveChanges();
                return RedirectToAction("Index", "CandidatePersonalInfProfile");
            }
            return View(CandidateLanguage);
        }

		[Authorize(Roles = "Candidate (Job Seeker)")]
		// GET: CandidateLanguage/Delete/5
		public ActionResult Delete(int? CandidateLanguageID,String Language)
        {

            ViewBag.Language = Language;
            if (CandidateLanguageID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateLanguage CandidateLanguage = db.CandidateLanguage.Find(CandidateLanguageID);
            if (CandidateLanguage == null)
            {
                return RedirectToAction("Index", "CandidatePersonalInfProfile");
            }
            return View(CandidateLanguage);
        }

		[Authorize(Roles = "Candidate (Job Seeker)")]
		// POST: CandidateLanguage/Delete/5
		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int CandidateLanguageID)
        {
            CandidateLanguage CandidateLanguage = db.CandidateLanguage.Find(CandidateLanguageID);
            db.CandidateLanguage.Remove(CandidateLanguage);
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
