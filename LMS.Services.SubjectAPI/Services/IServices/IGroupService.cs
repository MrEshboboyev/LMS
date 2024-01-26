using LMS.Services.SubjectAPI.Models.Dto;

namespace LMS.Services.SubjectAPI.Services.IServices
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupDto>> GetGroupsAsync();
    }
}
