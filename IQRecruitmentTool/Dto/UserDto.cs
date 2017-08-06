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

namespace IQRecruitmentTool.Dto
{
    public class UserDto
    {
        private readonly RecruitmentTestEntities _db = new RecruitmentTestEntities();

        public class RegisteredUsers
        {
            public Guid UserID { get; set; }
            public string FullName { get; set; }
            public string CellNumber { get; set; }
            public string IDNumber { get; set; }
        }

        public List<RegisteredUsers> GetUsers()
        {
            var results  = from c in _db.CandidatePersonalInfProfile.ToList()
                 select new RegisteredUsers
                 {
                     UserID = new Guid(c.UserID),
                     FullName = c.Name + " " + c.Surname,
                     CellNumber = c.CellphoneNumber,
                     IDNumber = c.CandidateIDNumber                

                 };

            return results.ToList();
        }
    }
}