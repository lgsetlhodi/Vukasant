using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IQRecruitmentTool.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IQRecruitmentTool.Controllers
{
    
    public class HomeController : Controller
    {
        ApplicationDbContext context;
        private RecruitmentTestEntities db = new RecruitmentTestEntities();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public HomeController()
        {
            context = new ApplicationDbContext();

        }

        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

		public ActionResult Policy()
		{
			return View();
		}
		public ActionResult UserAggreement()
		{
			return View();
		}
        public ActionResult EmailConfirm()
        {
            return View();
        }

        public ActionResult Index()
        {
           
            bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (val1 == true)
            {
                String UserID = User.Identity.GetUserId();
                var ListRoled = context.Users.Where(x => x.Id == UserID).SelectMany(x => x.Roles).ToList();
                if (ListRoled.Count > 1)
                {
                    return RedirectToAction("multiroles", "Home");

                }
                else if (ListRoled.SingleOrDefault().RoleId.ToString() == "9553ad5a-3d06-477d-8c32-2abe89b1070e")
                {

                    return RedirectToAction("Create", "CandidatePersonalInfProfile");



                }
                else if (ListRoled.SingleOrDefault().RoleId.ToString() == "3d7d43a1-138d-4bd6-a68b-5575e094fa18" || ListRoled.SingleOrDefault().RoleId.ToString() == "77504222-47c8-4652-86f6-83dc42dc861f" || ListRoled.SingleOrDefault().RoleId.ToString() == "52269ac1-c75d-4687-9db8-1ef6ba66624a")
                {

                    return RedirectToAction("Create", "StorageCompany");




                }
            }
            else
            {
           

            }
            return View();



        }
        public ActionResult Form()
        {
            return View();
        }
        public ActionResult Multirole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Form(EmailFormModel model)
        {
            //var response = Request["g-recaptcha-response"];
            //string secretkey = "6LfCdygUAAAAAOkmp5x7A5A4zjSz3c6j-c0Pci3J";
            //var client = new WebClient();
            //var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api.js/siteverify?secret={0}&response={1}", secretkey, response));
            //var obj = JObject.Parse(result);
            //var status = (bool)obj.SelectToken("success");
            //ViewBag.Message = status ? "reCaptcha Validation success" : "reCatcha Validation failed";
            //if (status)
            //{
            //    ViewBag.Message = "success";
            //}
            //else
            //{
            //    ViewBag.Message = "fail";
            //}
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("info@vukasantewaa.co.za"));  // replace with valid value 
                message.From = new MailAddress("info@vukasantewaa.co.za");  // replace with valid value
                message.Subject = "Your email subject";
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "info@vukasantewaa.co.za",  // replace with valid value
                        Password = "Info4321!"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "mail.vukasantewaa.co.za";
                    smtp.Port = 587;
                    smtp.EnableSsl = false;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
        

             return View(model); 
            
        }
        public ActionResult Sent()
        {
            return View();
        }

        public ActionResult ManageProfile()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Savedata(JobApplication JobApplication)
        {

            JobApplication Sect = new JobApplication();
            Sect.AppliedDate = System.DateTime.Today;
            Sect.AppUserID = User.Identity.GetUserId();
            Sect.JobID = JobApplication.JobID;
            db.JobApplication.Add(Sect);
            db.SaveChanges();


            return Json(JobApplication, JsonRequestBehavior.AllowGet);

        }
        [Authorize(Roles = "Candidate (Job Seeker)")]
        public ActionResult TestingJobstt(int? JobType, int? industryType)
        {
            List<object> Jobslist = new List<object>();
            if (industryType == null) {
       
                Jobslist.Add(db.vwJobs.Where(x => x.JobTypeID == JobType).ToList());
                Jobslist.Add(db.CompanyDescription.ToList());
                Jobslist.Add(db.VWJobType.Where(x => x.JobTypeID == JobType).ToList());
            }
            else
            {
           
                Jobslist.Add(db.vwJobs.Where(x => x.JobTypeID == JobType).ToList());
                Jobslist.Add(db.CompanyDescription.ToList());
                Jobslist.Add(db.VWJobType.Where(x => x.JobTypeID == JobType).ToList());
            }
            
            Jobslist.Add(db.vwJobs.ToList());
            Jobslist.Add(db.CompanyDescription.ToList());
            Jobslist.Add(db.VWJobType.ToList());


            return View(Jobslist);
        }
    }
}