using LMS.Services.SubjectAPI.Models.Dto;

namespace LMS.Services.SubjectAPI.Services.IServices
{
    public interface ISharedService
    {
        Task<IEnumerable<GroupSubjectDto>> GetGroupSubjects();
    }
}
