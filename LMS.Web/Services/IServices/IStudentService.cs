using LMS.Web.Models;

namespace LMS.Web.Services.IServices
{
    public interface IStudentService
    {
        Task<ResponseDto?> GetStudentAsync(string studentName);
        Task<ResponseDto?> GetAllStudentsAsync();
        Task<ResponseDto?> GetStudentByIdAsync(int id);
        Task<ResponseDto?> GetStudentsByGroupName(string groupName);
        Task<ResponseDto?> CreateStudentAsync(StudentDto studentDto);
        Task<ResponseDto?> UpdateStudentAsync(StudentDto studentDto);
        Task<ResponseDto?> DeleteStudentAsync(int id);
    }
}
