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
    
    public partial class UserIdeaSearchHistory
    {
        public int ID { get; set; }
        public System.Guid UserID { get; set; }
        public string SearchedTerm { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
    }
}
