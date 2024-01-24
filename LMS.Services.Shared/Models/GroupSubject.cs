using LMS.Services.Shared.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Services.Shared.Models
{
    public class GroupSubject
    {
        [Key]
        public int GroupSubjectId { get; set; }

        // connection properties
        public int GroupId { get; set; }
        [NotMapped]
        public GroupDto Group { get; set; }

        public int SubjectId { get; set; }
        [NotMapped]
        public SubjectDto Subject { get; set; }
    }
}
