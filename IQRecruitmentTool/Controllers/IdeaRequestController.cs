using IQRecruitmentTool.Dto;
using IQRecruitmentTool.ViewModel;
using IQRecruitmentTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Mvc;

namespace IQRecruitmentTool.Controllers
{
    public class IdeaRequestController : Controller
    {
        private readonly RecruitmentTestEntities _db = new RecruitmentTestEntities();

        private Guid userId = new Guid("FB944136-E6ED-4D6F-8481-A83C427AF132");//User.Identity.GetUserId();
        // GET: IdeaRequest
        public ActionResult Index()
        {     

            var userIdeasRequests = new IdeasDto();

            IdeasViewModel ivm = new IdeasViewModel();
            ivm.IdeaRequestsPending = userIdeasRequests.GetIdeaRequest().Where(u => u.IdeaRequesterId == userId && u.IsDeleted != true).ToList();
            ivm.IdeaRequestsCollabo = userIdeasRequests.GetIdeaRequest().Where(u => u.IdeaRequesterId == userId && u.RequestType == "Collaborate" && u.Status == "Accepted").ToList();
            ivm.IdeaRequestsFund = userIdeasRequests.GetIdeaRequest().Where(u => u.IdeaRequesterId == userId && u.RequestType == "Fund"  && u.Status == "Accepted").ToList();
            return View(ivm);
        }

        public ActionResult ActionRequest(IdeasDto.RequestDto requestobj)
        {
            var message = "";

            //CHECK TO SEE IF THE REQUEST REALLY EXISTS AND IT HAS NOT BEEN DELETED         
            var foundRequest = _db.IdeaRequest.FirstOrDefault(i => i.ID == requestobj.RequestId && i.UserID == userId && i.IsDeleted != true);

           

            if (foundRequest != null) //REQUEST WAS FOUND
            {
                foundRequest.LastModified = DateTime.Now;

                if (requestobj.Action == "Revoke")
                {
                    foundRequest.IsRevoked = true;

                    if (_db.SaveChanges() > 0)
                    {
                        message = "Request revoked";
                    }

                }
            }
            

           
            return Json(message);
        }
    }
}