using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IQRecruitmentTool.Dto;

namespace IQRecruitmentTool.ViewModel
{

    public class UserIdeaSearchViewed
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int UserIdeaId  { get; set; }
        public DateTime CreateDateTime { get; set; }
    }

    public class  UserIdeaSearchHistory
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string SearchTerm { get; set; }
        public DateTime CreateDateTime { get; set; }
    }

    public class IdeasViewModel
    {
        public List<IdeasDto.UserIdeas> RecentIdeas { get; set; }
        public List<IdeasDto.UserIdeas> ViewedIdeas { get; set; }
        public List<IdeasDto.UserIdeas> SearchedIdeas { get; set; }
        public List<IdeasDto.IdeaRequestDto> IdeaRequestsPending { get; set; }
        public List<IdeasDto.IdeaRequestDto> IdeaRequestsFund { get; set; }
        public List<IdeasDto.IdeaRequestDto> IdeaRequestsCollabo { get; set; }
        public string LastSearchedTerm  { get; set; }
    }
}