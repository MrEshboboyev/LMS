using LMS.Web.Models;

namespace LMS.Web.Services.IServices
{
    public interface ISubjectService
    {
        #region Get Subject(s)
        Task<ResponseDto?> GetSubjectAsync(string subjectName);
        Task<ResponseDto?> GetAllSubjectsAsync();
        Task<ResponseDto?> GetSubjectByIdAsync(int id);
        #endregion
        Task<ResponseDto?> CreateSubjectAsync(SubjectDto subjectDto);
        Task<ResponseDto?> UpdateSubjectAsync(SubjectDto subjectDto);
        Task<ResponseDto?> DeleteSubjectAsync(int id);
    }
}
