using LMS.Web.Models;
using LMS.Web.Services.IServices;
using LMS.Web.Utility;
using System.Xml.Linq;
using static LMS.Web.Utility.SD;

namespace LMS.Web.Services
{
    public class StudentService : IStudentService
    {
        // DI for IBaseService
        private readonly IBaseService _baseService;
        public StudentService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        #region Get Student(s)
        public Task<ResponseDto?> GetStudentAsync(string studentName)
        {
            return _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = SD.StudentAPIBase + "/api/student/" + $"GetByName/{studentName}"
            });
        }

        public Task<ResponseDto?> GetAllStudentsAsync()
        {
            return _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = SD.StudentAPIBase + "/api/student"
            });
        }
        
        public Task<ResponseDto?> GetStudentByIdAsync(int id)
        {
            return _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = SD.StudentAPIBase + "/api/student/" + id
            });
        }

        public Task<ResponseDto?> GetStudentsByGroupNameAsync(string groupName)
        {
            return _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.GET,
                Url = SD.StudentAPIBase + "/api/student/" + $"GetStudentsByGroupName/{groupName}"
            });
        }
        #endregion

        public Task<ResponseDto?> CreateStudentAsync(StudentDto studentDto)
        {
            return _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.POST,
                Data = studentDto,
                Url = SD.StudentAPIBase + "/api/student"
            });
        }

        public Task<ResponseDto?> DeleteStudentAsync(int id)
        {
            return _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.DELETE,
                Url = SD.StudentAPIBase + "/api/student/" + id
            });
        }

        public Task<ResponseDto?> UpdateStudentAsync(StudentDto studentDto)
        {
            return _baseService.SendAsync(new RequestDto()
            {
                ApiType = ApiType.PUT,
                Data = studentDto,
                Url = SD.StudentAPIBase + "/api/student"
            });
        }
    }
}
