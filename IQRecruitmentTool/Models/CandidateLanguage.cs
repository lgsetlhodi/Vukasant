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


    public partial class CandidateLanguage
    {
        public int CandidateLanguageID { get; set; }
        public string UserID { get; set; }
        public Nullable<int> Language { get; set; }
        public Nullable<int> Read { get; set; }
        public Nullable<int> Write { get; set; }
        public Nullable<int> Speak { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
    }
}
