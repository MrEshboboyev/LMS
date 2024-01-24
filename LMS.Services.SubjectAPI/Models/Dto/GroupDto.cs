namespace LMS.Services.SubjectAPI.Models.Dto
{
    public class GroupDto
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public List<GroupSubjectDto> GroupSubjects { get; set; }
    }
}
