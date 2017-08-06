using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc.Html;
using IQRecruitmentTool.Models;
using IQRecruitmentTool.Dto;

namespace IQRecruitmentTool.Dto
{

    public class IdeasDto
    {
        private readonly RecruitmentTestEntities _db = new RecruitmentTestEntities();
        private UserDto _userDto =  new UserDto();

        public class UserIdeas
        {
            public int IdeasId { get; set; }
            public Guid ? UserId { get; set; }
            public string UserName { get; set; }
            public int IndustryId { get; set; }
            public string Industry { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime ? CreatedDateTime { get; set; }
            public bool ? IsActive { get; set; }
        }

        public class IdeaRequestDto
        {
            public int IdeaRequestId { get; set; }
            public int IdeasId { get; set; }
            public Guid ? IdeaCreatorId { get; set; }
            public Guid ? IdeaRequesterId { get; set; }
            public string IdeaCreatorName { get; set; }
            public int IndustryId { get; set; }
            public string Industry { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public int StepNo { get; set; }
            public string Status { get; set; }
            public string RequestType { get; set; }
            public DateTime? IdeaCreatedDateTime { get; set; }
            public DateTime? IdeaRequestCreatedDateTime { get; set; }
            public bool ? IsDeleted { get; set; }
            public bool ? IsRevoked { get; set; }



        }

        public class RequestDto
        {
            public string Action { get; set; }
            public int IdeaId { get; set; }
            public int RequestId { get; set; }
        }

        public class IdeaRouteDto
        {
            public int RouteId { get; set; }
            public int RequestTypeId { get; set; }
            public string RequestType { get; set; }
            public int StepNo { get; set; }
            public string Status { get; set; }
        }

       
        

        public List<UserIdeas> GetIdeas()
        {
            var results = from i in _db.UserIdea.ToList()
                          join ind in _db.ListIndustry on i.IndustryID equals ind.IndustryID
                          join u in _userDto.GetUsers() on i.UserID equals u.UserID
                          select new UserIdeas
                          {

                              IdeasId = i.ID,
                              UserId = i.UserID,
                              UserName = u.FullName,
                              IndustryId = ind.IndustryID,
                              Industry = ind.Industry,
                              Title = i.Title,
                              Description = i.Description,
                              CreatedDateTime = i.CreatedDateTime,
                              IsActive = i.IsActive

                          };
            return results.ToList();

        }


        public List<IdeaRequestDto> GetIdeaRequest()
        {
            var results = from id in _db.IdeaRequest.ToList()
                          join ir in _db.IdeaRoute.ToList() on id.IdeaRouteID equals ir.ID
                          join irt in _db.IdeaRequestType.ToList() on ir.IdeaRequestTypeID equals irt.ID
                          join ui in _db.UserIdea.ToList() on id.UserIdeaID equals ui.ID
                          join u in _userDto.GetUsers() on ui.UserID equals u.UserID
                          select new IdeaRequestDto
                          {                           

                                IdeaRequestId  = id.ID,
                                IdeasId  = ui.ID,
                                Title = ui.Title,
                                Description = ui.Description,
                                IdeaCreatedDateTime = ui.CreatedDateTime,
                                IdeaRequestCreatedDateTime = id.CreatedDateTime,
                                IdeaCreatorId = ui.UserID,
                                IdeaCreatorName = u.FullName,
                                IdeaRequesterId = id.UserID,
                                StepNo = ir.StepNo,
                                Status = ir.Status,//id.IsRevoked == true ? "Revoked" : id.IsDeleted == true ? "Deleted" : ir.Status,
                                RequestType = irt.Request,
                                IsDeleted = id.IsDeleted,
                                IsRevoked = id.IsRevoked



                          };
            return results.ToList();

        }

        public List<IdeaRouteDto> GetIdeaRoutes()
        {
            var routes = from a in _db.IdeaRoute
                join b in _db.IdeaRequestType on a.IdeaRequestTypeID equals b.ID
                select new IdeaRouteDto
                {
                    RouteId = a.ID,
                    RequestTypeId = b.ID,
                    RequestType = b.Request,
                    StepNo = a.StepNo,
                    Status = a.Status
                    

                };
            return routes.ToList();
        }


        public List<UserIdeaSearchHistory> GetIdeaSearchHistory()
        {
            return _db.UserIdeaSearchHistory.ToList();
     
        }




    }

}

