using LMS.Services.GroupAPI.Models.Dto;
using LMS.Services.GroupAPI.Services.IServices;
using Newtonsoft.Json;

namespace LMS.Services.GroupAPI.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SubjectService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<SubjectDto>> GetSubjects()
        {
            var client = _httpClientFactory.CreateClient("Subject");
            var response = await client.GetAsync("/api/subject");
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            if(resp.IsSuccess && resp != null)
            {
                return JsonConvert.DeserializeObject<IEnumerable<SubjectDto>>(Convert.ToString(resp.Result));
            }

            return new List<SubjectDto>();
        }
    }
}
