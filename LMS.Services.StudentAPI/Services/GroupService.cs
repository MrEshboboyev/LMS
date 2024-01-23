using LMS.Services.StudentAPI.Models.Dto;
using LMS.Services.StudentAPI.Services.IServices;
using Newtonsoft.Json;

namespace LMS.Services.StudentAPI.Services
{
    public class GroupService : IGroupService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GroupService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<GroupDto>> GetGroupsAsync()
        {
            var client = _httpClientFactory.CreateClient("Group");
            var response = await client.GetAsync("/api/group");
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            if(resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<IEnumerable<GroupDto>>(Convert.ToString(resp.Result));
            }

            return new List<GroupDto>();
        }
    }
}
