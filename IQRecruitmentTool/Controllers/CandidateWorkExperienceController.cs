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
    public class CandidateWorkExperienceController : Controller
    {
        private RecruitmentTestEntities db = new RecruitmentTestEntities();

        // GET: CandidateWorkExperiences
        public ActionResult Index()
        {
            return View(db.CandidateWorkExperience.ToList());
        }

        // GET: CandidateWorkExperiences/Details/5
        public ActionResult Details(int? WorkExperienceID)
        {
            if (WorkExperienceID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateWorkExperience candidateWorkExperience = db.CandidateWorkExperience.Find(WorkExperienceID);
            if (candidateWorkExperience == null)
            {
                return HttpNotFound();
            }
            return View(candidateWorkExperience);
        }

        // GET: CandidateWorkExperiences/Create
        [Authorize]
        public ActionResult Create()
        {
			ViewBag.errorDate = "";
            ViewBag.Edit = "EditorFor";
            ViewBag.Category = new SelectList(db.ListIndustry,"IndustryID","Industry");
            ViewBag.EmploymentBasis = new SelectList(db.ListEmploymentBasis, "EmploymentBasisID", "EmploymentBasis");
            return View();
        }

        // POST: CandidateWorkExperiences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,JobTitle,CompanyName,Sector,EmploymentBasis,DescriptionOfDuties,JobStartDate,JobEndate,CurrentEmployment")] CandidateWorkExperience candidateWorkExperience)
        {
			ViewBag.errorDate = "";
			ViewBag.Category = new SelectList(db.ListIndustry, "IndustryID", "Industry");
			ViewBag.EmploymentBasis = new SelectList(db.ListEmploymentBasis, "EmploymentBasisID", "EmploymentBasis");
			if (candidateWorkExperience.CurrentEmployment == false && (candidateWorkExperience.JobEndate == null))
            {
				ViewBag.errorDate = "*";
				return View(candidateWorkExperience);

			}
			String UserID = User.Identity.GetUserId();
			candidateWorkExperience.UserID = UserID;
			candidateWorkExperience.UpdatedDate = System.DateTime.Now;

			if (ModelState.IsValid)
				
            {
                if (candidateWorkExperience.CurrentEmployment==true)
                {
                    if (db.CandidateWorkExperience.Any(x => x.CurrentEmployment == true && x.UserID== UserID && x.WorkExperienceID!= candidateWorkExperience.WorkExperienceID))
                    {
                        ViewBag.CurrentEmployment = "Please note that you are currently working at " + db.CandidateWorkExperience.Where(x => x.CurrentEmployment == true && x.UserID == UserID).SingleOrDefault().CompanyName.ToString() + " Please modify and try again";

                    }

                    else
                    {
                        candidateWorkExperience.UserID = User.Identity.GetUserId();
                        candidateWorkExperience.UpdatedDate = System.DateTime.Now;
                        db.CandidateWorkExperience.Add(candidateWorkExperience);
                        db.SaveChanges();
                        return RedirectToAction("Index", "CandidatePersonalInfProfile", new { area = "" });
                    }
                   
                }
                else
                {
                    candidateWorkExperience.UserID = User.Identity.GetUserId();
                    candidateWorkExperience.UpdatedDate = System.DateTime.Now;
                    db.CandidateWorkExperience.Add(candidateWorkExperience);
                    db.SaveChanges();
                    return RedirectToAction("Index", "CandidatePersonalInfProfile", new { area = "" });
                }
                
            }
            ViewBag.Category = new SelectList(db.ListIndustry, "IndustryID", "Industry");
            ViewBag.EmploymentBasis = new SelectList(db.ListEmploymentBasis, "EmploymentBasisID", "EmploymentBasis");
            return View(candidateWorkExperience);
        }

        // GET: CandidateWorkExperiences/Edit/5
        public ActionResult Edit(int? WorkExperienceID)
        {
			ViewBag.errorDate = "";
			ViewBag.Category = new SelectList(db.ListIndustry, "IndustryID", "Industry");
            ViewBag.EmploymentBasis = new SelectList(db.ListEmploymentBasis, "EmploymentBasisID", "EmploymentBasis");
            if (WorkExperienceID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateWorkExperience candidateWorkExperience = db.CandidateWorkExperience.Find(WorkExperienceID);
            if (candidateWorkExperience == null)
            {
                return HttpNotFound();
            }
            return View(candidateWorkExperience);
        }

        // POST: CandidateWorkExperiences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkExperienceID,UserID,JobTitle,CompanyName,Sector,EmploymentBasis,DescriptionOfDuties,JobStartDate,JobEndate,CurrentEmployment")] CandidateWorkExperience candidateWorkExperience)
        {
			ViewBag.errorDate = "";
			ViewBag.Category = new SelectList(db.ListIndustry, "IndustryID", "Industry");
			ViewBag.EmploymentBasis = new SelectList(db.ListEmploymentBasis, "EmploymentBasisID", "EmploymentBasis");
            if (candidateWorkExperience.CurrentEmployment == false && (candidateWorkExperience.JobEndate == null))
            {
				ViewBag.errorDate = "*";
				return View(candidateWorkExperience);

			}
			ViewBag.Edit = "EditorFor";
            ViewBag.Category = new SelectList(db.ListIndustry, "IndustryID", "Industry");
            ViewBag.EmploymentBasis = new SelectList(db.ListEmploymentBasis, "EmploymentBasisID", "EmploymentBasis");
            String UserID = User.Identity.GetUserId();
			candidateWorkExperience.UserID = UserID;
            candidateWorkExperience.UpdatedDate = System.DateTime.Now;
            if (ModelState.IsValid)
            {
				if (candidateWorkExperience.CurrentEmployment == true)
				{
					if (db.CandidateWorkExperience.Any(x => x.CurrentEmployment == true && x.UserID == UserID && x.WorkExperienceID!=candidateWorkExperience.WorkExperienceID))
					{
						ViewBag.CurrentEmployment = "Please note that you are currently working at " + db.CandidateWorkExperience.Where(x => x.CurrentEmployment == true).SingleOrDefault().CompanyName.ToString() + " Please modify and try again";

					}

					else
					{
						db.Entry(candidateWorkExperience).State = EntityState.Modified;
						db.SaveChanges();
						return RedirectToAction("Index", "CandidatePersonalInfProfile", new { area = "" });
					}

				}
				else
				{
					db.Entry(candidateWorkExperience).State = EntityState.Modified;
					db.SaveChanges();
					return RedirectToAction("Index", "CandidatePersonalInfProfile", new { area = "" });
				}
				
            }
            return View(candidateWorkExperience);
        }

        // GET: CandidateWorkExperiences/Delete/5
        public ActionResult Delete(int? WorkExperienceID)
        {
            if (WorkExperienceID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidateWorkExperience candidateWorkExperience = db.CandidateWorkExperience.Find(WorkExperienceID);
            if (candidateWorkExperience == null)
            {
                return HttpNotFound();
            }
            return View(candidateWorkExperience);
        }

        // POST: CandidateWorkExperiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int WorkExperienceID)
        {
            CandidateWorkExperience candidateWorkExperience = db.CandidateWorkExperience.Find(WorkExperienceID);
            db.CandidateWorkExperience.Remove(candidateWorkExperience);
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
