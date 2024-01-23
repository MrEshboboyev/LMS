using LMS.Services.StudentAPI.Models.Dto;

namespace LMS.Services.StudentAPI.Services.IServices
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupDto>> GetGroupsAsync();
    }
}
