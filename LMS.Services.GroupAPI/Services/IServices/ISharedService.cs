using LMS.Services.GroupAPI.Models.Dto;

namespace LMS.Services.GroupAPI.Services.IServices
{
    public interface ISharedService
    {
        Task<IEnumerable<GroupSubjectDto>> GetGroupSubjects();
    }
}
