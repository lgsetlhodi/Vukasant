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
using System.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Twilio.TwiML;



namespace IQRecruitmentTool.Controllers
{
    public class JobsController : Controller
    {
        private RecruitmentTestEntities db = new RecruitmentTestEntities();
        
       
    

  

        // GET: Jobs/Create
        public ActionResult Create(int? CompanyID)
        {
            List<SelectListItem> listitem = new List<SelectListItem>();
            List<SelectListItem> listitemdrop = new List<SelectListItem>();
            DropdownListValues dropSa = new DropdownListValues();
            ViewBag.CompanyID = CompanyID;
            Jobs model = new Jobs()
            {
                CompanyID = CompanyID
            };
            ViewBag.CustError = "";
            ViewBag.TownID = "";
            ViewBag.RateType = new SelectList(db.RateType, "RateID", "RateName");
            ViewBag.RemType = new SelectList(db.RemunerationType, "RemunerationTypeID", "RemunerationName");
            ViewBag.JobType = new SelectList(db.JobType, "JobTypeID", "JobType1");
            ViewBag.Categoryset = new SelectList(db.ListIndustry, "IndustryID", "Industry");
            ViewBag.Provinces = new SelectList(db.Province, "ProvinceID", "ProvinceName");
            ViewBag.Region = new SelectList(db.Region, "RegionID", "Region1");
            ViewBag.Town = new SelectList(db.Town, "TownID", "Town1");
            ViewBag.EmploymentBasis = new SelectList(db.ListEmploymentBasis, "EmploymentBasisID", "EmploymentBasis");
            ViewBag.JobLevelset = new SelectList(db.ListJobLevel, "JobLevelID", "JobLevel");

            DropdownListValues dropdrivers = new DropdownListValues();

            ViewBag.Dropdownlist = new SelectList(listitem, "Text", "Value");
            return View();
        }


        public JsonResult GetRegion(int id)
        {
            ViewBag.Region = new SelectList(db.Region.Where(x => x.ProvinceID == id), "RegionID", "Region1");



            return Json(ViewBag.Region);

        }
        public JsonResult GetTown(int id)
        {
            ViewBag.Town = new SelectList(db.Town.Where(x => x.RegionID == id), "TownID", "Town1");



            return Json(ViewBag.Town);

        }


        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RateType,JobTypeID,JobID,UserID,CategoryID,CompanyID,JobTitle,CompanyName,JobDescription,JobLevelID,RemunerationFrom,RemunerationTo,CandidateRequirements,TownID,EmploymentBasisID,NumberOfPositions,AdvertDateSartDate,NumberOfMonths,CapturedBy,CapturedDate,UpdatedBy,UpdatedDate,Active,CustomRemuneration,RemunerationType")] Jobs Job)
        {
            ViewBag.RateType = new SelectList(db.RateType, "RateID", "RateName");
            ViewBag.RemType = new SelectList(db.RemunerationType, "RemunerationTypeID", "RemunerationName");
            ViewBag.JobType = new SelectList(db.JobType, "JobTypeID", "JobType1");
            ViewBag.Categoryset = new SelectList(db.ListIndustry, "IndustryID", "Industry");
            ViewBag.Provinces = new SelectList(db.Province, "ProvinceID", "ProvinceName");
            ViewBag.Region = new SelectList(db.Region, "RegionID", "Region1");
            ViewBag.Town = new SelectList(db.Town, "TownID", "Town1");
            ViewBag.EmploymentBasis = new SelectList(db.ListEmploymentBasis, "EmploymentBasisID", "EmploymentBasis");
            ViewBag.JobLevelset = new SelectList(db.ListJobLevel, "JobLevelID", "JobLevel");
            String UserID = User.Identity.GetUserId();
            Job.UpdatedDate = System.DateTime.Today;
            Job.UpdatedBy = UserID;
            Job.UserID = UserID;
            Job.CapturedBy = UserID;
            ViewBag.CustError = "";
            ViewBag.TownID = "";
            if (Job.RemunerationType==3 && (Job.CustomRemuneration==null || Job.CustomRemuneration==""))
            {
                ViewBag.CustError = "*";
                return View(Job);

            }
            else
            {


            }
            if (Job.TownID == 0)
            {
                ViewBag.TownID = "*";
                return View(Job);

            }
            else
            {


            }


            if (ModelState.IsValid)
            {
                db.Jobs.Add(Job);
                db.SaveChanges();
                try
                {
                    String Number = db.StorageCompany.Where(x => x.CreatedBy == UserID).SingleOrDefault().MobileNumber;
                    string sub = Number.Substring(Math.Max(1, Number.Length - 9));
                    String realNum = "+27" + sub;
                    var AccountSID = ConfigurationManager.AppSettings["AccountSID"];
                    var AuthToken = ConfigurationManager.AppSettings["AuthToken"];
                    TwilioClient.Init(AccountSID, AuthToken);
                    var to = new PhoneNumber(realNum);
                    var from = new PhoneNumber("+15878003596");
                    var message = MessageResource.Create(
                        to: to,
                        from: from,
                        body: "Thank you for posting your job on Vukasantewaa your reference JobID is  " + Job.JobID + " and your advert date will start on the " + Job.AdvertDateSartDate.Value.ToString("yyyy MMM dd"));
                }
                catch 
                {
                    
                }
                
                return RedirectToAction("Index", "StorageCompany", new { area = "" });
            }

            return View(Job);
        }

        // GET: Jobs/Edit/5
        public ActionResult Edit(int? JobID,int CompanyID)
        {


            List<SelectListItem> listitem = new List<SelectListItem>();
            List<SelectListItem> listitemdrop = new List<SelectListItem>();
            DropdownListValues dropSa = new DropdownListValues();
            ViewBag.RateType = new SelectList(db.RateType, "RateID", "RateName");
            ViewBag.RemType = new SelectList(db.RemunerationType, "RemunerationTypeID", "RemunerationName");
            ViewBag.JobType = new SelectList(db.JobType, "JobTypeID", "JobType1");
            ViewBag.Categoryset = new SelectList(db.ListIndustry, "IndustryID", "Industry");
            ViewBag.Provinces = new SelectList(db.Province, "ProvinceID", "ProvinceName");
            ViewBag.Region = new SelectList(db.Region, "RegionID", "Region1");
            ViewBag.Town = new SelectList(db.Town, "TownID", "Town1");
            ViewBag.EmploymentBasis = new SelectList(db.ListEmploymentBasis, "EmploymentBasisID", "EmploymentBasis");
            ViewBag.JobLevelset = new SelectList(db.ListJobLevel, "JobLevelID", "JobLevel");
            ViewBag.CustError = "";
            ViewBag.TownID = "";
            DropdownListValues dropdrivers = new DropdownListValues();

            ViewBag.Dropdownlist = new SelectList(listitem, "Text", "Value");
            if (JobID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jobs Job = db.Jobs.Find(JobID);
            if (Job == null)
            {
                return HttpNotFound();
            }



			String TW = Convert.ToString(db.LocationView.Where(x => x.TownID == Job.TownID).SingleOrDefault().Town.ToString());
			String PR = db.LocationView.Where(x => x.TownID == Job.TownID).SingleOrDefault().ProvinceName.ToString();
			String RG = db.LocationView.Where(x => x.TownID == Job.TownID).SingleOrDefault().Region.ToString();

			int TWI = db.LocationView.Where(x => x.TownID == Job.TownID).SingleOrDefault().TownID;
			int? PRI = db.LocationView.Where(x => x.TownID == Job.TownID).SingleOrDefault().ProvinceID;
			int RGI = db.LocationView.Where(x => x.TownID == Job.TownID).SingleOrDefault().RegionID;

			ViewBag.InTown = (TW);
			ViewBag.InProvince = PR;
			ViewBag.InRegion = RG;
			ViewBag.InTownI = TWI;
			ViewBag.InProvinceI = PRI;
			ViewBag.InRegionI = RGI;



			ViewBag.Provinces = new SelectList(db.Province, "ProvinceID", "ProvinceName", PRI);
			ViewBag.Region = new SelectList(db.Region.Where(x => x.ProvinceID == PRI), "RegionID", "Region1", RGI);
			ViewBag.Town = new SelectList(db.Town.Where(x => x.RegionID == RGI), "TownID", "Town1", TWI);






            return View(Job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RateType,JobTypeID,JobID,UserID,CategoryID,CompanyID,JobTitle,CompanyName,JobDescription,JobLevelID,RemunerationFrom,RemunerationTo,CandidateRequirements,TownID,EmploymentBasisID,NumberOfPositions,AdvertDateSartDate,NumberOfMonths,CapturedBy,CapturedDate,UpdatedBy,UpdatedDate,Active,CustomRemuneration,RemunerationType")] Jobs Job)
        {

            ViewBag.RateType = new SelectList(db.RateType, "RateID", "RateName");
            ViewBag.RemType = new SelectList(db.RemunerationType, "RemunerationTypeID", "RemunerationName");
            ViewBag.JobType = new SelectList(db.JobType, "JobTypeID", "JobType1");
            ViewBag.Categoryset = new SelectList(db.ListIndustry, "IndustryID", "Industry");
            ViewBag.Provinces = new SelectList(db.Province, "ProvinceID", "ProvinceName");
            ViewBag.Region = new SelectList(db.Region, "RegionID", "Region1");
            ViewBag.Town = new SelectList(db.Town, "TownID", "Town1");
            ViewBag.EmploymentBasis = new SelectList(db.ListEmploymentBasis, "EmploymentBasisID", "EmploymentBasis");
            ViewBag.JobLevelset = new SelectList(db.ListJobLevel, "JobLevelID", "JobLevel");
            String UserID = User.Identity.GetUserId();
            Job.UpdatedDate = System.DateTime.Today;
            Job.UpdatedBy = UserID;
            Job.UserID = UserID;
            Job.CapturedBy = UserID;
            ViewBag.CustError = "";
            ViewBag.TownID = "";
           

           
            Job.UpdatedDate = System.DateTime.Today;
            Job.UpdatedBy = UserID;
            Job.UserID = UserID;

            Job.UpdatedDate = System.DateTime.Today;
            if (Job.RemunerationType == 3 && (Job.CustomRemuneration == null || Job.CustomRemuneration == ""))
            {
                ViewBag.CustError = "*";
                return View(Job);

            }
            else
            {


            }
            if (Job.TownID == 0)
            {
                ViewBag.TownID = "*";
                return View(Job);

            }
            else
            {


            }

            if (ModelState.IsValid)
            {
                db.Entry(Job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "StorageCompany", new { area = "" });
            }
            return View(Job);
        }

        // GET: Jobs/Delete/5
        public ActionResult Delete(int? JobID)
        {
            if (JobID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jobs Job = db.Jobs.Find(JobID);
            if (Job == null)
            {
                return HttpNotFound();
            }
            return View(Job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int JobID)
        {
            Jobs Job = db.Jobs.Find(JobID);
            db.Jobs.Remove(Job);
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
