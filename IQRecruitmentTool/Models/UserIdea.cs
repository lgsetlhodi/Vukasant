//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IQRecruitmentTool.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserIdea
    {
        public int ID { get; set; }
        public Nullable<System.Guid> UserID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> IndustryID { get; set; }
    }
}
