using LMS.Web.Models;
using LMS.Web.Services.IServices;
using LMS.Web.Utility;
using static LMS.Web.Utility.SD;

namespace LMS.Web.Services
{
    public class SubjectService : ISubjectService
    {
        // DI for IBaseService
        private readonly IBaseService _baseService;
        public SubjectService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public Task<ResponseDto?> CreateSubjectAsync(SubjectDto subjectDto)
        {
            return _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = subjectDto,
                Url = SD.SubjectAPIBase + "/api/subject"
            });
        }

        public Task<ResponseDto?> DeleteSubjectAsync(int id)
        {
            return _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.DELETE,
                Url = SD.SubjectAPIBase + "/api/subject/" + id
            });
        }

        public Task<ResponseDto?> GetAllSubjectsAsync()
        {
            return _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = SD.SubjectAPIBase + "/api/subject"
            });
        }

        public Task<ResponseDto?> GetSubjectAsync(string subjectName)
        {
            return _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = SD.SubjectAPIBase + "/api/subject/" + $"GetByName/{subjectName}"
            });
        }

        public Task<ResponseDto?> GetSubjectByIdAsync(int id)
        {
            return _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = SD.SubjectAPIBase + "/api/subject/" + id
            });
        }

        public Task<ResponseDto?> UpdateSubjectAsync(SubjectDto subjectDto)
        {
            return _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.PUT,
                Data = subjectDto,
                Url = SD.SubjectAPIBase + "/api/subject"
            });
        }
    }
}
