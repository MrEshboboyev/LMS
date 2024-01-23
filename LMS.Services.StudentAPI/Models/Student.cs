using LMS.Services.StudentAPI.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Services.StudentAPI.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [Required]
        public string Name { get; set; }
        public int GroupId { get; set; }
        [NotMapped]
        public GroupDto? Group { get; set; }
    }
}
