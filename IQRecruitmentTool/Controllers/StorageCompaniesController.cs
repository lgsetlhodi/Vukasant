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
    public class StorageCompanyController : Controller
    {
        private RecruitmentTestEntities db = new RecruitmentTestEntities();

        // GET: StorageCompany
        [Authorize(Roles = "Company, Admin,Recruitment Agency,Small Business")]
        public ActionResult Index()
        {
            String UserID = User.Identity.GetUserId();
            int CompanyID = db.StorageCompany.Where(x => x.CreatedBy == UserID).Select(x => x.CompanyID).Single();
            String Name = db.vwCompanyDetailed.Where(x => x.CreatedBy == UserID).SingleOrDefault().CompanyName.ToString();
            ViewBag.NameCompany = Name;
            List<object> CompanyDets = new List<object>();
            //var Jobtype =  db.Jobs.Where(x => x.CompanyID == CompanyID).SingleOrDefault().JobTypeID.Value;
            CompanyDets.Add(db.vwCompanyDetailed.Where(x => x.CreatedBy == UserID).ToList());
            CompanyDets.Add(db.vwJobs.Where(x => x.CompanyID == CompanyID).OrderByDescending(x=> x.UpdatedDate).ToList());
            CompanyDets.Add(db.CompanyDescription.Where(x => x.CompanyID == CompanyID).ToList());
            CompanyDets.Add(db.VWJobType.Where(x=> x.CompanyID==CompanyID).ToList());
            CompanyDets.Add(db.vwJobApplicantsCount.Where(x => x.CompanyID == CompanyID).ToList());

            return View(CompanyDets);
        }
        [Authorize(Roles = "Company, Admin,Recruitment Agency,Small Business")]
        public ActionResult CandidateDetails(int JobID)
        {
            String UserID = User.Identity.GetUserId();
            int CompanyID = db.StorageCompany.Where(x => x.CreatedBy == UserID).Select(x => x.CompanyID).Single();
            String Name = db.vwCompanyDetailed.Where(x => x.CreatedBy == UserID).SingleOrDefault().CompanyName.ToString();
            ViewBag.NameCompany = Name;
            List<object> CompanyDets = new List<object>();
            //var Jobtype =  db.Jobs.Where(x => x.CompanyID == CompanyID).SingleOrDefault().JobTypeID.Value;
  
            CompanyDets.Add(db.vwApplicantsPerJob.Where(x => x.JobID == JobID).ToList());

            return View(CompanyDets);
        }
        public ActionResult CompPriceList()
        {
         
            return View();
        }
        public ActionResult TendPriceList()
        {

            return View();
        }
        


        // GET: StorageCompany/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorageCompany StorageCompany = db.StorageCompany.Find(id);
            if (StorageCompany == null)
            {
                return HttpNotFound();
            }
            return View(StorageCompany);
        }

        // GET: StorageCompany/Create
        [Authorize(Roles = "Company, Admin,Recruitment Agency,Small Business")]
        public ActionResult Create()
        {
			ViewBag.CName = "";
			ViewBag.CReg = "";
			ViewBag.BusSect = "";
			ViewBag.MobNum = "";
			ViewBag.ComNum = "";
			ViewBag.CompAdd = "";
			ViewBag.CompSiz = "";
			ViewBag.CompDesc = "";
			ViewBag.CompBee = "";
			ViewBag.YinB = "";
			ViewBag.TWID = "";
			String UserID = User.Identity.GetUserId();
            if (db.StorageCompany.Any(p => p.CreatedBy == UserID))
            {
                int CompanyID = db.StorageCompany.Where(x => x.CreatedBy == UserID).Select(x => x.CompanyID).Single();
                return RedirectToAction("Index", "StorageCompany", new { CompanyID = CompanyID });

            }
            else
            {
                ViewBag.BeeLevel = new SelectList(db.BEELevel, "BEELevelID", "BEELevelName");
                ViewBag.Categoryset = new SelectList(db.ListIndustry, "IndustryID", "Industry");
                ViewBag.MyCountries = new SelectList(db.ListCountry, "CountryID", "Country");
                ViewBag.Provinces = new SelectList(db.Province, "ProvinceID", "ProvinceName");
                ViewBag.Region = new SelectList(db.Region, "RegionID", "Region1");
                ViewBag.Town = new SelectList(db.Town, "TownID", "Town1");

            }
            return View();
        }

        // POST: StorageCompany/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Company, Admin,Recruitment Agency,Small Business")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyID,MobileNumber,CompanyName,CompanyRegistrationNumber,BusinessSector,CompanyTelephoneNumber,CompanyEmail,CompanyCountry,TownID,CompanyAddress,CompanySize,YearsInBusiness,Industry,CreateDate,BeeLevel,UpdateDate,CompanyDescription")] StorageCompany StorageCompany)
        {
            int ErrorCountRecruit = 0;
            int ErrorCountCompany = 0;
            int ErrorCountSmallB = 0;
            ViewBag.CName = "";
            ViewBag.CReg = "";
            ViewBag.BusSect = "";
            ViewBag.MobNum = "";
            ViewBag.ComNum = "";
            ViewBag.CompAdd = "";
            ViewBag.CompSiz = "";
            ViewBag.CompDesc = "";
            ViewBag.CompBee = "";
            ViewBag.YinB = "";
			ViewBag.TWID = "";


            String UserName = User.Identity.GetUserName();
            StorageCompany.CompanyEmail = UserName;
            String UserId = User.Identity.GetUserId();
            var Companydescriptions = new CompanyDescription();
            ViewBag.BeeLevel = new SelectList(db.BEELevel, "BEELevelID", "BEELevelName");
            ViewBag.Categoryset = new SelectList(db.ListIndustry, "IndustryID", "Industry");
            ViewBag.MyCountries = new SelectList(db.ListCountry, "CountryID", "Country");
            ViewBag.Provinces = new SelectList(db.Province, "ProvinceID", "ProvinceName");
            ViewBag.Region = new SelectList(db.Region, "RegionID", "Region1");
            ViewBag.Town = new SelectList(db.Town, "TownID", "Town1");
            ViewBag.Categoryset = new SelectList(db.ListIndustry, "IndustryID", "Industry");
            //ViewBag.MyRegions = new SelectList(db.Regions, "RegionID", "Region");
            StorageCompany.CreatedBy = User.Identity.GetUserId();
            StorageCompany.CreateDate = DateTime.Now;
            StorageCompany.UpdateDate = DateTime.Now;

			if (StorageCompany.TownID == null || StorageCompany.TownID == 0)
			{
				ViewBag.TWID = "*";
				ErrorCountCompany = ErrorCountCompany + 1;
				ErrorCountSmallB = ErrorCountSmallB + 1;
			    ErrorCountRecruit = ErrorCountRecruit + 1;

			}
			if (StorageCompany.CompanyName == null || StorageCompany.CompanyName == "")
            {
                ViewBag.CName = "*";
                ErrorCountRecruit = ErrorCountRecruit + 1;
                ErrorCountCompany = ErrorCountCompany + 1;
                ErrorCountSmallB = ErrorCountSmallB + 1;

            }
            if (StorageCompany.CompanyRegistrationNumber == null || StorageCompany.CompanyRegistrationNumber == "")
            {
                ViewBag.CReg = "*";
                ErrorCountRecruit = ErrorCountRecruit + 1;
                ErrorCountCompany = ErrorCountCompany + 1;
                ErrorCountSmallB = ErrorCountSmallB + 1;

            }
            if (StorageCompany.BusinessSector == null || StorageCompany.BusinessSector == 0)
            {
                ViewBag.BusSect = "*";
                ErrorCountCompany = ErrorCountCompany + 1;
                ErrorCountSmallB = ErrorCountSmallB + 1;

            }
            if (StorageCompany.MobileNumber == null || StorageCompany.MobileNumber == "")
            {
                ViewBag.MobNum = "*"; ;
                ErrorCountRecruit = ErrorCountRecruit + 1;
                ErrorCountCompany = ErrorCountCompany + 1;
                ErrorCountSmallB = ErrorCountSmallB + 1;

            }
            if (StorageCompany.CompanyTelephoneNumber == null || StorageCompany.CompanyTelephoneNumber == "")
            {
                ViewBag.ComNum = "*";
                ErrorCountRecruit = ErrorCountRecruit + 1;
                ErrorCountCompany = ErrorCountCompany + 1;
                ErrorCountSmallB = ErrorCountSmallB + 1;

            }
            if (StorageCompany.CompanyAddress == null || StorageCompany.CompanyAddress == "")
            {
                ViewBag.CompAdd = "*";
                ErrorCountRecruit = ErrorCountRecruit + 1;
                ErrorCountCompany = ErrorCountCompany + 1;
                ErrorCountSmallB = ErrorCountSmallB + 1;

            }
            if (StorageCompany.CompanySize == null || StorageCompany.CompanySize == "")
            {
                ViewBag.CompSiz = "*";
                ErrorCountSmallB = ErrorCountSmallB + 1;

            }
            if (StorageCompany.CompanyDescription == null || StorageCompany.CompanyDescription == "")
            {
				ViewBag.CompDesc = "*";
                ErrorCountRecruit = ErrorCountRecruit + 1;
                ErrorCountCompany = ErrorCountCompany + 1;
                ErrorCountSmallB = ErrorCountSmallB + 1;
            }
            if (StorageCompany.BeeLevel == null || StorageCompany.BeeLevel == 0)
            {
				ViewBag.CompBee = "*";
                ErrorCountSmallB = ErrorCountSmallB + 1;

            }
            if (StorageCompany.YearsInBusiness == null || StorageCompany.YearsInBusiness == 0)
            {
				ViewBag.YinB = "*";
                ErrorCountSmallB = ErrorCountSmallB + 1;

            }
            if (Request.IsAuthenticated && User.IsInRole("Recruitment Agency"))
            { 
                if(ErrorCountRecruit==0)
                {
                    db.StorageCompany.Add(StorageCompany);
                    db.SaveChanges();
                    int CompanyID = StorageCompany.CompanyID;
                    Companydescriptions.CompanyID = CompanyID;
                    Companydescriptions.Description = StorageCompany.CompanyDescription;
                    db.CompanyDescription.Add(Companydescriptions);
                    db.SaveChanges();
                    return RedirectToAction("Index", "StorageCompany", new { CompanyID = StorageCompany.CompanyID });

                }
                else
                {
                    return View(StorageCompany);

                }

            }
            else if(Request.IsAuthenticated && User.IsInRole("Company"))
            {
                if (ErrorCountCompany == 0)
                {
                    db.StorageCompany.Add(StorageCompany);
                    db.SaveChanges();
                    int CompanyID = StorageCompany.CompanyID;
                    Companydescriptions.CompanyID = CompanyID;
                    Companydescriptions.Description = StorageCompany.CompanyDescription;
                    db.CompanyDescription.Add(Companydescriptions);
                    db.SaveChanges();
                    return RedirectToAction("Index", "StorageCompany", new { CompanyID = StorageCompany.CompanyID });

                }
                else
                {
                    return View(StorageCompany);

                }

            }
            else if(Request.IsAuthenticated && User.IsInRole("Small Business"))
            {
                if (ErrorCountSmallB == 0)
                {
                    db.StorageCompany.Add(StorageCompany);
                    db.SaveChanges();
                    int CompanyID = StorageCompany.CompanyID;
                    Companydescriptions.CompanyID = CompanyID;
                    Companydescriptions.Description = StorageCompany.CompanyDescription;
                    db.CompanyDescription.Add(Companydescriptions);
                    db.SaveChanges();
                    return RedirectToAction("Index", "StorageCompany", new { CompanyID = StorageCompany.CompanyID });

                }
                else
                {
                    return View(StorageCompany);

                }

            }
            return View(StorageCompany);

        }

        // GET: StorageCompany/Edit/5
        public ActionResult Edit(int? CompanyId, string imgUrl)
		{
			ViewBag.CName = "";
			ViewBag.CReg = "";
			ViewBag.BusSect = "";
			ViewBag.MobNum = "";
			ViewBag.ComNum = "";
			ViewBag.CompAdd = "";
			ViewBag.CompSiz = "";
			ViewBag.CompDesc = "";
			ViewBag.CompBee = "";
			ViewBag.YinB = "";
			ViewBag.TWID = "";
			//ViewBag.MyRegions = new SelectList(db.Regions, "RegionID", "Region1");
			if (CompanyId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorageCompany StorageCompany = db.StorageCompany.Find(CompanyId);
            int DescriptionID = db.CompanyDescription.Where(x => x.CompanyID == CompanyId).SingleOrDefault().DescriptionID;
            CompanyDescription CompanyDesc = db.CompanyDescription.Find(DescriptionID);
            StorageCompany.CompanyDescription = CompanyDesc.Description;
            if (StorageCompany == null)
            {
                return HttpNotFound();
            }
            if (StorageCompany.CreatedBy == User.Identity.GetUserId())
            {


                String TW = Convert.ToString(db.LocationView.Where(x => x.TownID == StorageCompany.TownID).SingleOrDefault().Town.ToString());
                String PR = db.LocationView.Where(x => x.TownID == StorageCompany.TownID).SingleOrDefault().ProvinceName.ToString();
                String RG = db.LocationView.Where(x => x.TownID == StorageCompany.TownID).SingleOrDefault().Region.ToString();

                int TWI = db.LocationView.Where(x => x.TownID == StorageCompany.TownID).SingleOrDefault().TownID;
                int? PRI = db.LocationView.Where(x => x.TownID == StorageCompany.TownID).SingleOrDefault().ProvinceID;
                int RGI = db.LocationView.Where(x => x.TownID == StorageCompany.TownID).SingleOrDefault().RegionID;

                ViewBag.InTown = (TW);
                ViewBag.InProvince = PR;
                ViewBag.InRegion = RG;
                ViewBag.InTownI = TWI;
                ViewBag.InProvinceI = PRI;
                ViewBag.InRegionI = RGI;

                List<SelectListItem> listitem = new List<SelectListItem>();
                List<SelectListItem> listitemdrop = new List<SelectListItem>();
                DropdownListValues dropSa = new DropdownListValues();
				ViewBag.BeeLevel = new SelectList(db.BEELevel, "BEELevelID", "BEELevelName");
				ViewBag.Raceset = new SelectList(db.ListRace, "RaceID", "Race");
                ViewBag.Genderset = new SelectList(db.Gender, "GenderID", "Gender1");
                ViewBag.Disabilityset = new SelectList(db.ListDisability, "DisabilityID", "Disability");
                ViewBag.Provinces = new SelectList(db.Province, "ProvinceID", "ProvinceName", PRI);
                ViewBag.Region = new SelectList(db.Region.Where(x => x.ProvinceID == PRI), "RegionID", "Region1", RGI);
                ViewBag.Town = new SelectList(db.Town.Where(x => x.RegionID == RGI), "TownID", "Town1", TWI);
                ViewBag.Categoryset = new SelectList(db.ListIndustry, "IndustryID", "Industry");
            }
            else
            {

            }
                return View(StorageCompany);
        }

        // POST: StorageCompany/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompanyID,MobileNumber,CompanyName,CompanyRegistrationNumber,BusinessSector,CompanyTelephoneNumber,CompanyEmail,CompanyCountry,TownID,CompanyAddress,CompanySize,YearsInBusiness,Industry,CreateDate,BeeLevel,UpdateDate,imgUrl,CompanyDescription")] StorageCompany StorageCompany)
        {
            int ErrorCountRecruit = 0;
            int ErrorCountCompany = 0;
            int ErrorCountSmallB = 0;
            ViewBag.CName = "";
            ViewBag.CReg = "";
            ViewBag.BusSect = "";
            ViewBag.MobNum = "";
            ViewBag.ComNum = "";
            ViewBag.CompAdd = "";
            ViewBag.CompSiz = "";
            ViewBag.CompDesc = "";
            ViewBag.CompBee = "";
            ViewBag.YinB = "";
			ViewBag.TWID = "";
			if (StorageCompany.TownID == 0|| StorageCompany.TownID == null)
			{
				 ErrorCountRecruit = ErrorCountRecruit+1;
				 ErrorCountCompany = ErrorCountCompany+1;
				 ErrorCountSmallB = ErrorCountSmallB+1;
				ViewBag.TWID = "*";
				ViewBag.Provinces = new SelectList(db.Province, "ProvinceID", "ProvinceName");
				ViewBag.Region = new SelectList(db.Region, "RegionID", "Region1","Select Region");
				ViewBag.Town = new SelectList(db.Town, "TownID", "Town1", "Select Town");

			}
			else

			{
				String TW = Convert.ToString(db.LocationView.Where(x => x.TownID == StorageCompany.TownID).SingleOrDefault().Town.ToString());
				String PR = db.LocationView.Where(x => x.TownID == StorageCompany.TownID).SingleOrDefault().ProvinceName.ToString();
				String RG = db.LocationView.Where(x => x.TownID == StorageCompany.TownID).SingleOrDefault().Region.ToString();

				int TWI = db.LocationView.Where(x => x.TownID == StorageCompany.TownID).SingleOrDefault().TownID;
				int? PRI = db.LocationView.Where(x => x.TownID == StorageCompany.TownID).SingleOrDefault().ProvinceID;
				int RGI = db.LocationView.Where(x => x.TownID == StorageCompany.TownID).SingleOrDefault().RegionID;

				ViewBag.InTown = (TW);
				ViewBag.InProvince = PR;
				ViewBag.InRegion = RG;
				ViewBag.InTownI = TWI;
				ViewBag.InProvinceI = PRI;
				ViewBag.InRegionI = RGI;
				ViewBag.Provinces = new SelectList(db.Province, "ProvinceID", "ProvinceName", PRI);
				ViewBag.Region = new SelectList(db.Region.Where(x => x.ProvinceID == PRI), "RegionID", "Region1", RGI);
				ViewBag.Town = new SelectList(db.Town.Where(x => x.RegionID == RGI), "TownID", "Town1", TWI);
			
			}

			
			List<SelectListItem> listitem = new List<SelectListItem>();
			List<SelectListItem> listitemdrop = new List<SelectListItem>();
			DropdownListValues dropSa = new DropdownListValues();
			ViewBag.BeeLevel = new SelectList(db.BEELevel, "BEELevelID", "BEELevelName");
			ViewBag.Raceset = new SelectList(db.ListRace, "RaceID", "Race");
			ViewBag.Genderset = new SelectList(db.Gender, "GenderID", "Gender1");
			ViewBag.Disabilityset = new SelectList(db.ListDisability, "DisabilityID", "Disability");
			ViewBag.Categoryset = new SelectList(db.ListIndustry, "IndustryID", "Industry");

			int DescriptionID = db.CompanyDescription.Where(x => x.CompanyID == StorageCompany.CompanyID).SingleOrDefault().DescriptionID;
            CompanyDescription CompanyDesc = db.CompanyDescription.Find(DescriptionID);
            CompanyDesc.Description = StorageCompany.CompanyDescription;
            StorageCompany.UpdateDate = DateTime.Now;
            String UserID = User.Identity.GetUserId();
            StorageCompany.CreatedBy = UserID;
            if (StorageCompany.CompanyName == null || StorageCompany.CompanyName == "")
            {
                ViewBag.CName = "*";
                ErrorCountRecruit = ErrorCountRecruit + 1;
                ErrorCountCompany = ErrorCountCompany + 1;
                ErrorCountSmallB = ErrorCountSmallB + 1;

            }
            if (StorageCompany.CompanyRegistrationNumber == null || StorageCompany.CompanyRegistrationNumber == "")
            {
                ViewBag.CReg = "*";
                ErrorCountRecruit = ErrorCountRecruit + 1;
                ErrorCountCompany = ErrorCountCompany + 1;
                ErrorCountSmallB = ErrorCountSmallB + 1;

            }
            if (StorageCompany.BusinessSector == null || StorageCompany.BusinessSector == 0)
            {
                ViewBag.BusSect = "*";
                ErrorCountCompany = ErrorCountCompany + 1;
                ErrorCountSmallB = ErrorCountSmallB + 1;

            }
            if (StorageCompany.MobileNumber == null || StorageCompany.MobileNumber == "")
            {
                ViewBag.MobNum = "*"; ;
                ErrorCountRecruit = ErrorCountRecruit + 1;
                ErrorCountCompany = ErrorCountCompany + 1;
                ErrorCountSmallB = ErrorCountSmallB + 1;

            }
            if (StorageCompany.CompanyTelephoneNumber == null || StorageCompany.CompanyTelephoneNumber == "")
            {
                ViewBag.ComNum = "*";
                ErrorCountRecruit = ErrorCountRecruit + 1;
                ErrorCountCompany = ErrorCountCompany + 1;
                ErrorCountSmallB = ErrorCountSmallB + 1;

            }
            if (StorageCompany.CompanyAddress == null || StorageCompany.CompanyAddress == "")
            {
                ViewBag.CompAdd = "*";
                ErrorCountRecruit = ErrorCountRecruit + 1;
                ErrorCountCompany = ErrorCountCompany + 1;
                ErrorCountSmallB = ErrorCountSmallB + 1;

            }
            if (StorageCompany.CompanySize == null || StorageCompany.CompanySize == "")
            {
                ViewBag.CompSiz = "*";
                ErrorCountSmallB = ErrorCountSmallB + 1;

            }
            if (StorageCompany.CompanyDescription == null || StorageCompany.CompanyDescription == "")
            {
				ViewBag.CompDesc = "*";
                ErrorCountRecruit = ErrorCountRecruit + 1;
                ErrorCountCompany = ErrorCountCompany + 1;
                ErrorCountSmallB = ErrorCountSmallB + 1;
            }
            if (StorageCompany.BeeLevel == null || StorageCompany.BeeLevel == 0)
            {
				ViewBag.CompBee = "*";
                ErrorCountSmallB = ErrorCountSmallB + 1;

            }
            if (StorageCompany.YearsInBusiness == null || StorageCompany.YearsInBusiness == 0)
            {
				ViewBag.YinB = "*";
                ErrorCountSmallB = ErrorCountSmallB + 1;

            }
            if (Request.IsAuthenticated && User.IsInRole("Recruitment Agency"))
            {
                if (ErrorCountRecruit == 0)
                {
                    db.Entry(StorageCompany).State = EntityState.Modified;
                    db.Entry(CompanyDesc).State = EntityState.Modified;
                    db.SaveChanges();
                    int CompanyID = StorageCompany.CompanyID;

                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                else
                {
                    return View(StorageCompany);

                }

            }
            else if (Request.IsAuthenticated && User.IsInRole("Company"))
            {
                if (ErrorCountCompany == 0)
                {
                    db.Entry(StorageCompany).State = EntityState.Modified;
                    db.Entry(CompanyDesc).State = EntityState.Modified;
                    db.SaveChanges();
                    int CompanyID = StorageCompany.CompanyID;

                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                else
                {
                    return View(StorageCompany);

                }

            }
            else if (Request.IsAuthenticated && User.IsInRole("Small Business"))
            {
                if (ErrorCountSmallB == 0)
                {
                    db.Entry(StorageCompany).State = EntityState.Modified;
                    db.Entry(CompanyDesc).State = EntityState.Modified;
                    db.SaveChanges();
                    int CompanyID = StorageCompany.CompanyID;

                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                else
                {
                    return View(StorageCompany);

                }

            }
            return View(StorageCompany);

        
         
        }

        // GET: StorageCompany/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StorageCompany StorageCompany = db.StorageCompany.Find(id);
            if (StorageCompany == null)
            {
                return HttpNotFound();
            }
            return View(StorageCompany);
        }

        // POST: StorageCompany/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StorageCompany StorageCompany = db.StorageCompany.Find(id);
            db.StorageCompany.Remove(StorageCompany);
            db.SaveChanges();
            return RedirectToAction("Index");
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
