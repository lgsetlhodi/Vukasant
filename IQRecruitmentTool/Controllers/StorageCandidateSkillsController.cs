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
    public class CandidateSkillController : Controller
    {
        private RecruitmentTestEntities db = new RecruitmentTestEntities();

		[Authorize(Roles = "Candidate (Job Seeker)")]
		// GET: CandidateSkill
		public ActionResult Index()
        {
            return View(db.CandidateSkill.ToList());
        }

		[Authorize(Roles = "Candidate (Job Seeker)")]
		// GET: CandidateSkill/Details/5
		public ActionResult Details(int? CompetencyID)
        {
            if (CompetencyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateSkill CandidateSkill = db.CandidateSkill.Find(CompetencyID);
            if (CandidateSkill == null)
            {
                return RedirectToAction("Index", "CandidatePersonalInfProfile");
            }
            return View(CandidateSkill);
        }

		[Authorize(Roles = "Candidate (Job Seeker)")]
		// GET: CandidateSkill/Create
		public ActionResult Create()
        {
            ViewBag.Competency = new SelectList(db.Competency, "CompetencyID", "Competency1");
            return View();
        }

		[Authorize(Roles = "Candidate (Job Seeker)")]
		// POST: CandidateSkill/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecordID,Skill,Competency,Experience")] CandidateSkill CandidateSkill)
        {
            ViewBag.Competency = new SelectList(db.Competency, "CompetencyID", "Competency1");
            CandidateSkill.UpdateDate = DateTime.Now;
            CandidateSkill.UserID = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                db.CandidateSkill.Add(CandidateSkill);
        
                db.SaveChanges();
                return RedirectToAction("Index", "CandidatePersonalInfProfile");
            }

            return View(CandidateSkill);
        }

		[Authorize(Roles = "Candidate (Job Seeker)")]
		// GET: CandidateSkill/Edit/5
		public ActionResult Edit(int? RecordID)
        {
            ViewBag.Competency = new SelectList(db.Competency, "CompetencyID", "Competency1");
            if (RecordID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateSkill CandidateSkill = db.CandidateSkill.Find(RecordID);
            if (CandidateSkill == null)
            {
                return RedirectToAction("Index", "CandidatePersonalInfProfile");
            }
            return View(CandidateSkill);
        }

		[Authorize(Roles = "Candidate (Job Seeker)")]
		// POST: CandidateSkill/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecordID,UserID,Skill,Competency,Experience")] CandidateSkill CandidateSkill)
        {
            CandidateSkill.UserID = User.Identity.GetUserId();
            CandidateSkill.UpdateDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(CandidateSkill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "CandidatePersonalInfProfile");
            }
            return View(CandidateSkill);
        }

        [Authorize(Roles = "Candidate (Job Seeker)")]
        // GET: CandidateSkill/Delete/5
        public ActionResult Delete(int? RecordID)
        {
            if (RecordID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateSkill CandidateSkill = db.CandidateSkill.Find(RecordID);
            if (CandidateSkill == null)
            {
                return RedirectToAction("Index", "CandidatePersonalInfProfile");
            }
            return View(CandidateSkill);
        }

		[Authorize(Roles = "Candidate (Job Seeker)")]
		// POST: CandidateSkill/Delete/5

		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int RecordID)
        {
            CandidateSkill CandidateSkill = db.CandidateSkill.Find(RecordID);
            db.CandidateSkill.Remove(CandidateSkill);
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
