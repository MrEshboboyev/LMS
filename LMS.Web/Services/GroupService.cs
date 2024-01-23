using LMS.Web.Models;
using LMS.Web.Services.IServices;
using LMS.Web.Utility;
using System.Xml.Linq;
using static LMS.Web.Utility.SD;

namespace LMS.Web.Services
{
    public class GroupService : IGroupService
    {
        // DI for IBaseService
        private readonly IBaseService _baseService;
        public GroupService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public Task<ResponseDto?> CreateGroupAsync(GroupDto groupDto)
        {
            return _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = groupDto,
                Url = SD.GroupAPIBase + "/api/group"
            });
        }

        public Task<ResponseDto?> DeleteGroupAsync(int id)
        {
            return _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.DELETE,
                Url = SD.GroupAPIBase + "/api/group/" + id
            });
        }

        public Task<ResponseDto?> GetAllGroupsAsync()
        {
            return _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = SD.GroupAPIBase + "/api/group"
            });
        }

        public Task<ResponseDto?> GetGroupAsync(string groupName)
        {
            return _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = SD.GroupAPIBase + "/api/group/" + $"GetByName/{groupName}"
            });
        }

        public Task<ResponseDto?> GetGroupByIdAsync(int id)
        {
            return _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = SD.GroupAPIBase + "/api/group/" + id
            });
        }

        public Task<ResponseDto?> UpdateGroupAsync(GroupDto groupDto)
        {
            return _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.PUT,
                Data = groupDto,
                Url = SD.GroupAPIBase + "/api/group"
            });
        }
    }
}
