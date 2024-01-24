namespace LMS.Services.Shared.Models.Dto
{
    public class GroupSubjectDto
    {
        public int GroupSubjectId { get; set; }

        // connection properties
        public int GroupId { get; set; }
        public GroupDto Group { get; set; }

        public int SubjectId { get; set; }
        public SubjectDto Subject { get; set; }
    }
}
