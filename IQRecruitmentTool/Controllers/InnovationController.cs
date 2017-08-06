using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IQRecruitmentTool.Dto;
using IQRecruitmentTool.ViewModel;
using IQRecruitmentTool.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using UserIdeaSearchHistory = IQRecruitmentTool.Models.UserIdeaSearchHistory;

namespace IQRecruitmentTool.Controllers
{
    
    public class InnovationController : Controller
    {
        private readonly RecruitmentTestEntities _db = new RecruitmentTestEntities();
        
        // GET: Innovation
        public ActionResult Index()
        {
            var userId = new Guid("AD350B67-86E3-4AFD-955F-1315B111EAFD");//User.Identity.GetUserId();
          
            var userIdeas = new IdeasDto();
    
            IdeasViewModel ivm = new IdeasViewModel();

            //TAKE THE MOST RECENT (LAST) IDEA SEARCHED BY THE CURRENTLY LOGGED IN USER
            var userSearchedIdeas =  _db.UserIdeaSearchHistory.Where(u => u.UserID == userId).Take(1).OrderByDescending(s =>s.ID).FirstOrDefault();
    
            //TAKE 5 RECENT IDEAS VIEWED BY THE USER CURRENTLY LOGGED IN
            var userViewedIdeas = _db.UserIdeaViewed.Where(uvi => uvi.UserID == userId).Take(5).OrderByDescending(uvi => uvi.ID).Select(uvi => uvi.UserIdeaID);

            //var requestedIdeas = new IdeasDto().GetIdeaRequest().Where(ri => ri.IdeaRequesterId == userId).Select(i => i.IdeasId);


            //ALL IDEAS
            ivm.RecentIdeas = userIdeas.GetIdeas().Take(10).OrderByDescending(ri => ri.IdeasId).ToList();

            //TODO: Ensure that the system pulls all ideas except my own??
    
           


            if (userViewedIdeas.Any())
            {
                foreach (var ideas in userViewedIdeas)
                {
                    //BASED ON USER PREFERENCES AND SEARCHES
                    ivm.ViewedIdeas = userIdeas.GetIdeas().Where(vi => vi.IdeasId.ToString().Contains(ideas.ToString()) && vi.UserId == userId).Take(10).OrderByDescending(vi => vi.IdeasId).ToList();
                }
               
               
            }
                        

            if (userSearchedIdeas != null)
            {
                ivm.LastSearchedTerm = userSearchedIdeas.SearchedTerm;             
                ivm.SearchedIdeas = userIdeas.GetIdeas().Where(si => si.Title.Contains(userSearchedIdeas.SearchedTerm) && si.UserId == userId).Take(10).OrderByDescending(si => si.IdeasId).ToList();
            }
          

            return View(ivm);
        }

        public ActionResult CreateRequest(IdeasDto.RequestDto requestobj)
        {
            var userId = new Guid("FB944136-E6ED-4D6F-8481-A83C427AF132");  //User.Identity.GetUserId();
            var message = "";

            var foundRequest = new IdeasDto().GetIdeaRequest().FirstOrDefault(i => i.IdeaRequesterId== userId && i.IdeasId == requestobj.IdeaId);
            if (foundRequest != null)
            {
                message = foundRequest.RequestType + " request already sent for this project.";
                return Json(message);
            }

            var obj = new IdeaRequest();
            obj.UserID = userId;
            obj.UserIdeaID = requestobj.IdeaId;
            obj.CreatedDateTime = DateTime.Now;
            obj.LastModified = DateTime.Now;


            //DYNAMICALLY FIND THE FIRST ROUTE FOR THE REQUEST TYPE - THIS IS THE FIRST STEP FOR THE PROCESS
            var route = new IdeasDto();
            var foundRoute = route.GetIdeaRoutes().FirstOrDefault(r => r.StepNo == 1 && r.RequestType == requestobj.Action);


            if (requestobj.Action == "Collaborate")
            {
                if (foundRoute != null)
                {
                    obj.IdeaRouteID = foundRoute.RouteId;

                }
                
                _db.IdeaRequest.Add(obj);
                if (_db.SaveChanges() > 0)
                {
                    message = "Collaboration request submitted";
                }

               
            }

            if (requestobj.Action == "Fund")
            {
                if (foundRoute != null)
                {
                    obj.IdeaRouteID = foundRoute.RouteId;

                }

                _db.IdeaRequest.Add(obj);
                if (_db.SaveChanges() > 0)
                {
                    message = "Funding request submitted";
                }

            }
            return Json(message);
        }

        public ActionResult SearchIdeas(string searchTerm)
        {
            var userId = new Guid("AD350B67-86E3-4AFD-955F-1315B111EAFD");


            if (searchTerm != null)
             {
                string[] searchTerms = searchTerm.Split(',');

                foreach (var idea in searchTerms)
                {
                    var userSearch =
                        new IdeasDto().GetIdeaSearchHistory()
                            .Where(us => us.UserID == userId && us.SearchedTerm.ToLower() == searchTerm.ToLower());
                    if (!userSearch.Any())
                    {
                        var userSearchObj = new UserIdeaSearchHistory();
                        userSearchObj.UserID = userId;
                        userSearchObj.SearchedTerm = searchTerm;
                        userSearchObj.CreatedDateTime = DateTime.Now;
                        _db.UserIdeaSearchHistory.Add(userSearchObj);
                        _db.SaveChanges();

                    }
                   var userIdeas = new IdeasDto().GetIdeas().Where(u => u.Title.ToLower().Contains(idea.ToLower())).ToList();
                    return PartialView("_IdeaSearchPartial",userIdeas);
                }

            }

            return null;


        }

        
       
    }
}