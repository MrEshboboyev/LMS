﻿namespace LMS.Services.SubjectAPI.Models.Dto
{
    public class SubjectDto
    {
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Credits { get; set; }
        public List<GroupSubjectDto> GroupSubjects { get; set; }
    }
}
