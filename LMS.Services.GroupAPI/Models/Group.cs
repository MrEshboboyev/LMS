using LMS.Services.GroupAPI.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Services.GroupAPI.Models
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public List<GroupSubjectDto> GroupSubjects { get; set; }
    }
}
