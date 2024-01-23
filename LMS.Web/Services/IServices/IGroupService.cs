using LMS.Web.Models;

namespace LMS.Web.Services.IServices
{
    public interface IGroupService
    {
        Task<ResponseDto?> GetGroupAsync(string groupName);
        Task<ResponseDto?> GetAllGroupsAsync();
        Task<ResponseDto?> GetGroupByIdAsync(int id);
        Task<ResponseDto?> CreateGroupAsync(GroupDto groupDto);
        Task<ResponseDto?> UpdateGroupAsync(GroupDto groupDto);
        Task<ResponseDto?> DeleteGroupAsync(int id);
    }
}
