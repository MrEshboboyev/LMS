using LMS.Services.SubjectAPI.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Services.SubjectAPI.Models
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }

        // Basic Information
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public string Description { get; set; }

        // Additional Information
        [Required]
        public int Credits { get; set; }

        [NotMapped]
        public List<GroupSubjectDto> GroupSubjects { get; set; }
    }

}
