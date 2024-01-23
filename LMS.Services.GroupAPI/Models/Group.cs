using System.ComponentModel.DataAnnotations;

namespace LMS.Services.GroupAPI.Models
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }
        public string Name { get; set; }
    }
}
