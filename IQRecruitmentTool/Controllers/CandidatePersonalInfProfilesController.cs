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
    public class CandidatePersonalInfProfileController : Controller
    {
        private RecruitmentTestEntities db = new RecruitmentTestEntities();

	
		// GET: CandidatePersonalInfProfile
		[Authorize(Roles = "Candidate (Job Seeker)")]
        public ActionResult Index()
        {
            String UserID = User.Identity.GetUserId();


            String Name = db.CandidatePersonalInfProfile.Where(x => x.UserID == UserID).SingleOrDefault().Name.ToString();
            String Surname = db.CandidatePersonalInfProfile.Where(x => x.UserID == UserID).SingleOrDefault().Surname.ToString();

            ViewBag.Name = Name;
            ViewBag.Surname = Surname;
            List<object> CandProfInfo = new List<object>();
            CandProfInfo.Add(db.PersonalInfoVW.Where(x => x.UserID == UserID).ToList());
            CandProfInfo.Add(db.vwQualificationView.Where(x => x.UserID == UserID).OrderByDescending(x=>x.YearCompleted).ToList());
            CandProfInfo.Add(db.CandidateWorkExperience.Where(x => x.UserID == UserID).OrderByDescending(x => x.JobStartDate).ToList());
            CandProfInfo.Add(db.vwLanguageCompetency.Where(x => x.UserID == UserID).OrderByDescending(x=>x.UpdateDate).ToList());
            CandProfInfo.Add(db.vwDocumentLibrary.Where(x => x.UserID == UserID).OrderByDescending(x => x.UpdateDate).ToList());
            CandProfInfo.Add(db.vwSkills.Where(x => x.UserID == UserID).OrderByDescending(x => x.UpdateDate).ToList());
            CandProfInfo.Add(db.vwListedJobs.ToList());
			CandProfInfo.Add(db.vwJobsPerApplicant.Where(x => x.UserID == UserID).OrderByDescending(x => x.ApplicationDate).ToList());

            return View(CandProfInfo);
        }


		[Authorize(Roles = "Candidate (Job Seeker)")]
		// GET: CandidatePersonalInfProfile/Details/5
		public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidatePersonalInfProfile CandidatePersonalInfProfile = db.CandidatePersonalInfProfile.Find(id);
            if (CandidatePersonalInfProfile == null)
            {
                return HttpNotFound();
            }
            return View(CandidatePersonalInfProfile);
        }
        [HttpPost]
        public ActionResult LogOff()
        {


            return View();
        }
        public ActionResult JobDetail(int JobID)
        {
            List<object> JobApp = new List<object>();
            JobApp.Add(db.vwJobs.Where(x=> x.JobID==JobID).ToList());
            return View(JobApp);
        }

        public ActionResult JobApplication(int JobID)
        {
            ViewBag.Message = "";
            String UserID = User.Identity.GetUserId();
            JobApplication Sect = new JobApplication();
           if(db.JobApplication.Any(x=> x.JobID==JobID && x.AppUserID== UserID))
            {
                ViewBag.Message = "You have already applied for this Job";
            }
           else
            {
                ViewBag.Message = "Your have successfully applied for this job";
                Sect.AppliedDate = System.DateTime.Today;
            Sect.AppUserID = User.Identity.GetUserId();
            Sect.JobID = JobID;
            db.JobApplication.Add(Sect);
            db.SaveChanges();
            return View();
            }
            return View();
        }

        [Authorize(Roles = "Candidate (Job Seeker)")]
		// GET: CandidatePersonalInfProfile/Create
	
        public ActionResult Create()
        {

            String UserID = User.Identity.GetUserId();
            if (db.CandidatePersonalInfProfile.Any(p => p.UserID == UserID))
            {
                int Ident = db.CandidatePersonalInfProfile.Where(x => x.UserID == UserID).Select(x => x.ID).Single();
                return RedirectToAction("Index", "CandidatePersonalInfProfile", new { id = Ident });

            }
            else
            {
                List<SelectListItem> listitem = new List<SelectListItem>();
                List<SelectListItem> listitemdrop = new List<SelectListItem>();
                DropdownListValues dropSa = new DropdownListValues();
                ViewBag.Race = new SelectList(db.ListRace, "RaceID", "Race");
                ViewBag.Gender = new SelectList(db.Gender, "GenderID", "Gender1");
                ViewBag.Disability = new SelectList(db.ListDisability, "DisabilityID", "Disability");
                ViewBag.Provinces = new SelectList(db.Province, "ProvinceID", "ProvinceName");
                ViewBag.Region = new SelectList(db.Region, "RegionID", "Region1");
                ViewBag.Town = new SelectList(db.Town, "TownID", "Town1");

                DropdownListValues dropdrivers = new DropdownListValues();

                //dropdrivers.id = 0;
                //dropdrivers.Values = "Do you have a drivers license?";
                //listitemdrop.Add(new SelectListItem() { Value = dropdrivers.Values, Text = dropdrivers.id.ToString() });
                dropdrivers.id = 1;
                dropdrivers.Values = "Yes";
                listitemdrop.Add(new SelectListItem() { Value = dropdrivers.Values, Text = dropdrivers.id.ToString() });
                dropdrivers.id = 2;
                dropdrivers.Values = "No";
                listitemdrop.Add(new SelectListItem() { Value = dropdrivers.Values, Text = dropdrivers.id.ToString() });

                ViewBag.dropdrivers = new SelectList(listitemdrop, "Text", "Value");

                //dropSa.id = 0;
                //dropSa.Values = "Are you a South African Citizen";
                //listitem.Add(new SelectListItem() { Value = dropSa.Values, Text = dropSa.id.ToString() });
                dropSa.id = 1;
                dropSa.Values = "Yes";
                listitem.Add(new SelectListItem() { Value = dropSa.Values, Text = dropSa.id.ToString() });
                dropSa.id = 2;
                dropSa.Values = "No";
                listitem.Add(new SelectListItem() { Value = dropSa.Values, Text = dropSa.id.ToString() });

                ViewBag.Dropdownlist = new SelectList(listitem, "Text", "Value");
                return View();
            }

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


		[Authorize(Roles = "Candidate (Job Seeker)")]
		// POST: CandidatePersonalInfProfile/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,Name,Surname,RSACitizen,CandidateIDNumber,Race,Gender,Disability,TownID,CellphoneNumber,TelephoneNumber,StreetAddress,DriversLicense")] CandidatePersonalInfProfile CandidatePersonalInfProfile)
        {
            DropdownListValues dropdrivers = new DropdownListValues();
            List<SelectListItem> listitem = new List<SelectListItem>();
            List<SelectListItem> listitemdrop = new List<SelectListItem>();
            DropdownListValues dropSa = new DropdownListValues();
            String UserID = User.Identity.GetUserId();
            CandidatePersonalInfProfile.EmailAddress = User.Identity.GetUserName();
            CandidatePersonalInfProfile.UserID = UserID;
            CandidatePersonalInfProfile.UpdatedDate = System.DateTime.Today;
            if (ModelState.IsValid == false)
            {

                if (db.CandidatePersonalInfProfile.Any(p => p.UserID == UserID))
                {
                    var Ident = db.CandidatePersonalInfProfile.Where(x => x.UserID == UserID).Select(x => x.ID).ToList();
                    return RedirectToAction("Index", "CandidatePersonalInfProfile", new { id = Ident });

                }
                

            }
            else
            {
                db.CandidatePersonalInfProfile.Add(CandidatePersonalInfProfile);


                db.SaveChanges();
                return RedirectToAction("Index");

            }
            ViewBag.Race = new SelectList(db.ListRace, "RaceID", "Race");
            ViewBag.Gender = new SelectList(db.Gender, "GenderID", "Gender1");
            ViewBag.Disability = new SelectList(db.ListDisability, "DisabilityID", "Disability");
            ViewBag.Provinces = new SelectList(db.Province, "ProvinceID", "ProvinceName");
            ViewBag.Region = new SelectList(db.Region, "RegionID", "Region1");
            ViewBag.Town = new SelectList(db.Town, "TownID", "Town1");

            dropdrivers.id = 0;
            dropdrivers.Values = "Do you have a drivers license?";
            listitemdrop.Add(new SelectListItem() { Value = dropdrivers.Values, Text = dropdrivers.id.ToString() });
            dropdrivers.id = 1;
            dropdrivers.Values = "Yes";
            listitemdrop.Add(new SelectListItem() { Value = dropdrivers.Values, Text = dropdrivers.id.ToString() });
            dropdrivers.id = 2;
            dropdrivers.Values = "No";
            listitemdrop.Add(new SelectListItem() { Value = dropdrivers.Values, Text = dropdrivers.id.ToString() });

            ViewBag.dropdrivers = new SelectList(listitemdrop, "Text", "Value");

            dropSa.id = 0;
            dropSa.Values = "Are you a South African Citizen";
            listitem.Add(new SelectListItem() { Value = dropSa.Values, Text = dropSa.id.ToString() });
            dropSa.id = 1;
            dropSa.Values = "Yes";
            listitem.Add(new SelectListItem() { Value = dropSa.Values, Text = dropSa.id.ToString() });
            dropSa.id = 2;
            dropSa.Values = "No";
            listitem.Add(new SelectListItem() { Value = dropSa.Values, Text = dropSa.id.ToString() });

            ViewBag.Dropdownlist = new SelectList(listitem, "Text", "Value");

            return View(CandidatePersonalInfProfile);
        }

		[Authorize(Roles = "Candidate (Job Seeker)")]
		// GET: CandidatePersonalInfProfile/Edit/5
		[Authorize]
        public ActionResult Edit(int? id)
        {

            if (id == null || id<1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidatePersonalInfProfile CandidatePersonalInfProfile = db.CandidatePersonalInfProfile.Find(id);
            if (CandidatePersonalInfProfile == null)
            {
                return HttpNotFound();
            }

            if (CandidatePersonalInfProfile.UserID == User.Identity.GetUserId())
            {

              
                String TW = Convert.ToString(db.LocationView.Where(x => x.TownID == CandidatePersonalInfProfile.TownID).SingleOrDefault().Town.ToString());
               String PR = db.LocationView.Where(x => x.TownID == CandidatePersonalInfProfile.TownID).SingleOrDefault().ProvinceName.ToString();
               String RG =  db.LocationView.Where(x => x.TownID == CandidatePersonalInfProfile.TownID).SingleOrDefault().Region.ToString();

                int TWI  = db.LocationView.Where(x => x.TownID == CandidatePersonalInfProfile.TownID).SingleOrDefault().TownID;
                int? PRI = db.LocationView.Where(x => x.TownID == CandidatePersonalInfProfile.TownID).SingleOrDefault().ProvinceID;
                int RGI= db.LocationView.Where(x => x.TownID == CandidatePersonalInfProfile.TownID).SingleOrDefault().RegionID;

                ViewBag.InTown = (TW);
                ViewBag.InProvince = PR;
                ViewBag.InRegion = RG;
                ViewBag.InTownI = TWI;
                ViewBag.InProvinceI = PRI;
                ViewBag.InRegionI = RGI;
          
                List<SelectListItem> listitem = new List<SelectListItem>();
                List<SelectListItem> listitemdrop = new List<SelectListItem>();
                DropdownListValues dropSa = new DropdownListValues();
                ViewBag.Raceset = new SelectList(db.ListRace, "RaceID", "Race");
                ViewBag.Genderset = new SelectList(db.Gender, "GenderID", "Gender1");
                ViewBag.Disabilityset = new SelectList(db.ListDisability, "DisabilityID", "Disability");
                ViewBag.Provinces = new SelectList(db.Province, "ProvinceID", "ProvinceName",PRI);
                ViewBag.Region = new SelectList(db.Region.Where(x => x.ProvinceID == PRI), "RegionID", "Region1",RGI);
                ViewBag.Town = new SelectList(db.Town.Where(x => x.RegionID == RGI), "TownID", "Town1",TWI);
                
                DropdownListValues dropdrivers = new DropdownListValues();

                //dropdrivers.id = 0;
                //dropdrivers.Values = "Do you have a drivers license?";
                //listitemdrop.Add(new SelectListItem() { Value = dropdrivers.Values, Text = dropdrivers.id.ToString() });
                dropdrivers.id = 1;
                dropdrivers.Values = "Yes";
                listitemdrop.Add(new SelectListItem() { Value = dropdrivers.Values, Text = dropdrivers.id.ToString() });
                dropdrivers.id = 2;
                dropdrivers.Values = "No";
                listitemdrop.Add(new SelectListItem() { Value = dropdrivers.Values, Text = dropdrivers.id.ToString() });
              
                ViewBag.dropdrivers = new SelectList(listitemdrop, "Text", "Value");

                //dropSa.id = 0;
                //dropSa.Values = "Are you a South African Citizen";
                //listitem.Add(new SelectListItem() { Value = dropSa.Values, Text = dropSa.id.ToString() });
                dropSa.id = 1;
                dropSa.Values = "Yes";
                listitem.Add(new SelectListItem() { Value = dropSa.Values, Text = dropSa.id.ToString() });
                dropSa.id = 2;
                dropSa.Values = "No";
                listitem.Add(new SelectListItem() { Value = dropSa.Values, Text = dropSa.id.ToString() });

                ViewBag.Dropdownlist = new SelectList(listitem, "Text", "Value");
                return View(CandidatePersonalInfProfile);

            }
            else
            {
                FormsAuthentication.SignOut();
                Roles.DeleteCookie();
                Session.Clear();

                return RedirectToAction("Login", "Account");

            }

        }

		[Authorize(Roles = "Candidate (Job Seeker)")]
		// POST: CandidatePersonalInfProfile/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,Name,Surname,RSACitizen,CandidateIDNumber,Race,Gender,Disability,CellphoneNumber,TelephoneNumber,EmailAddress,StreetAddress,DriversLicense,TownID")] CandidatePersonalInfProfile CandidatePersonalInfProfile)
        {
            String TW = Convert.ToString(db.LocationView.Where(x => x.TownID == CandidatePersonalInfProfile.TownID).SingleOrDefault().Town.ToString());
            String PR = db.LocationView.Where(x => x.TownID == CandidatePersonalInfProfile.TownID).SingleOrDefault().ProvinceName.ToString();
            String RG = db.LocationView.Where(x => x.TownID == CandidatePersonalInfProfile.TownID).SingleOrDefault().Region.ToString();

            int TWI = db.LocationView.Where(x => x.TownID == CandidatePersonalInfProfile.TownID).SingleOrDefault().TownID;
            int? PRI = db.LocationView.Where(x => x.TownID == CandidatePersonalInfProfile.TownID).SingleOrDefault().ProvinceID;
            int RGI = db.LocationView.Where(x => x.TownID == CandidatePersonalInfProfile.TownID).SingleOrDefault().RegionID;

            ViewBag.InTown = (TW);
            ViewBag.InProvince = PR;
            ViewBag.InRegion = RG;
            ViewBag.InTownI = TWI;
            ViewBag.InProvinceI = PRI;
            ViewBag.InRegionI = RGI;

            List<SelectListItem> listitem = new List<SelectListItem>();
            List<SelectListItem> listitemdrop = new List<SelectListItem>();
            DropdownListValues dropSa = new DropdownListValues();
            ViewBag.Raceset = new SelectList(db.ListRace, "RaceID", "Race");
            ViewBag.Genderset = new SelectList(db.Gender, "GenderID", "Gender1");
            ViewBag.Disabilityset = new SelectList(db.ListDisability, "DisabilityID", "Disability");
            ViewBag.Provinces = new SelectList(db.Province, "ProvinceID", "ProvinceName", PRI);
            ViewBag.Region = new SelectList(db.Region.Where(x => x.ProvinceID == PRI), "RegionID", "Region1", RGI);
            ViewBag.Town = new SelectList(db.Town.Where(x => x.RegionID == RGI), "TownID", "Town1", TWI);

            DropdownListValues dropdrivers = new DropdownListValues();

            //dropdrivers.id = 0;
            //dropdrivers.Values = "Do you have a drivers license?";
            //listitemdrop.Add(new SelectListItem() { Value = dropdrivers.Values, Text = dropdrivers.id.ToString() });
            dropdrivers.id = 1;
            dropdrivers.Values = "Yes";
            listitemdrop.Add(new SelectListItem() { Value = dropdrivers.Values, Text = dropdrivers.id.ToString() });
            dropdrivers.id = 2;
            dropdrivers.Values = "No";
            listitemdrop.Add(new SelectListItem() { Value = dropdrivers.Values, Text = dropdrivers.id.ToString() });

            ViewBag.dropdrivers = new SelectList(listitemdrop, "Text", "Value");

            //dropSa.id = 0;
            //dropSa.Values = "Are you a South African Citizen";
            //listitem.Add(new SelectListItem() { Value = dropSa.Values, Text = dropSa.id.ToString() });
            dropSa.id = 1;
            dropSa.Values = "Yes";
            listitem.Add(new SelectListItem() { Value = dropSa.Values, Text = dropSa.id.ToString() });
            dropSa.id = 2;
            dropSa.Values = "No";
            listitem.Add(new SelectListItem() { Value = dropSa.Values, Text = dropSa.id.ToString() });

            ViewBag.Dropdownlist = new SelectList(listitem, "Text", "Value");

            CandidatePersonalInfProfile.UserID = User.Identity.GetUserId();

            CandidatePersonalInfProfile.UpdatedDate = System.DateTime.Today;
            if (ModelState.IsValid)
            {
               
                db.Entry(CandidatePersonalInfProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(CandidatePersonalInfProfile);
        }

		[Authorize(Roles = "Candidate (Job Seeker)")]
		// GET: CandidatePersonalInfProfile/Delete/5
		public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandidatePersonalInfProfile CandidatePersonalInfProfile = db.CandidatePersonalInfProfile.Find(id);
            if (CandidatePersonalInfProfile == null)
            {
                return HttpNotFound();
            }
            return View(CandidatePersonalInfProfile);
        }

		[Authorize(Roles = "Candidate (Job Seeker)")]
		// POST: CandidatePersonalInfProfile/Delete/5
		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CandidatePersonalInfProfile CandidatePersonalInfProfile = db.CandidatePersonalInfProfile.Find(id);
            db.CandidatePersonalInfProfile.Remove(CandidatePersonalInfProfile);
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
