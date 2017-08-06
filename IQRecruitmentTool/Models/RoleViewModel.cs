using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IQRecruitmentTool.Models
{
    public class RoleViewModel
    {
        public RoleViewModel() { }
        public RoleViewModel(ApplicationRole role)
        {
            Id = role.Id;
            Name = role.Name;

        }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}