using LMS.Services.GroupAPI.Models.Dto;

namespace LMS.Services.GroupAPI.Services.IServices
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectDto>> GetSubjects(); 
    }
}
