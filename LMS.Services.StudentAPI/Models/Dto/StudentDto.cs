namespace LMS.Services.StudentAPI.Models.Dto
{
    public class StudentDto
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
        public GroupDto? Group { get; set; }
    }
}
