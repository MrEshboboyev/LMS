using LMS.Services.GroupAPI.Models.Dto;
using LMS.Services.GroupAPI.Services.IServices;
using Newtonsoft.Json;

namespace LMS.Services.GroupAPI.Services
{
    public class SharedService : ISharedService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SharedService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<GroupSubjectDto>> GetGroupSubjects()
        {
            var client = _httpClientFactory.CreateClient("Shared");
            var response = await client.GetAsync("/api/shared");
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            if(resp.IsSuccess && resp != null)
            {
                return JsonConvert.DeserializeObject<IEnumerable<GroupSubjectDto>>(Convert.ToString(resp.Result));
            }

            return new List<GroupSubjectDto>();
        }
    }
}
