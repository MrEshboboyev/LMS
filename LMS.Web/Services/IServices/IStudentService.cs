using LMS.Web.Models;

namespace LMS.Web.Services.IServices
{
    public interface IStudentService
    {
        #region Get Student(s)
        Task<ResponseDto?> GetStudentAsync(string studentName);
        Task<ResponseDto?> GetAllStudentsAsync();
        Task<ResponseDto?> GetStudentByIdAsync(int id);
        Task<ResponseDto?> GetStudentsByGroupNameAsync(string groupName);
        #endregion
        Task<ResponseDto?> CreateStudentAsync(StudentDto studentDto);
        Task<ResponseDto?> UpdateStudentAsync(StudentDto studentDto);
        Task<ResponseDto?> DeleteStudentAsync(int id);
    }
}
