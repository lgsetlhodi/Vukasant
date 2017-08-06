using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IQRecruitmentTool.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace IQRecruitmentTool.Controllers
{
    public class ManagementProfileController : Controller
    {
        RecruitmentTestEntities db = new RecruitmentTestEntities();
        // GET: ManagementProfile
        [Authorize]
        public ActionResult Index()
        {
            List<object> CandidateDetails = new List<object>();
            CandidateDetails.Add(db.PersonalInfoVW.Where(x=>x.UserID== User.Identity.GetUserId()));
            
            return View();
        }
    }
}