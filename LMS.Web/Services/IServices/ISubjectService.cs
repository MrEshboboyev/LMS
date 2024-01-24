using LMS.Web.Models;

namespace LMS.Web.Services.IServices
{
    public interface ISubjectService
    {
        Task<ResponseDto?> GetSubjectAsync(string subjectName);
        Task<ResponseDto?> GetAllSubjectsAsync();
        Task<ResponseDto?> GetSubjectByIdAsync(int id);
        Task<ResponseDto?> CreateSubjectAsync(SubjectDto subjectDto);
        Task<ResponseDto?> UpdateSubjectAsync(SubjectDto subjectDto);
        Task<ResponseDto?> DeleteSubjectAsync(int id);
    }
}
