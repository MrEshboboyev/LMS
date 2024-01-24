using AutoMapper;
using LMS.Services.SubjectAPI.Data;
using LMS.Services.SubjectAPI.Models;
using LMS.Services.SubjectAPI.Models.Dto;
using LMS.Services.SubjectAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Services.SubjectAPI.Controllers
{
    [Route("api/subject")]
    [ApiController]
    public class SubjectAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ISharedService _sharedService;
        private IMapper _mapper;
        private ResponseDto _response;
        public SubjectAPIController(AppDbContext db,
            IMapper mapper,
            ISharedService sharedService)
        {
            _db = db;
            _sharedService = sharedService;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        // Get All Entities (Subjects)
        [HttpGet]
        public async Task<ResponseDto> Get()
        {
            try
            {
                IEnumerable<Subject> objList = _db.Subjects.ToList();

                // get all groupSubjects from Shared project
                IEnumerable<GroupSubjectDto> groupSubjects = await _sharedService.GetGroupSubjects();

                foreach (Subject obj in objList)
                {
                    obj.GroupSubjects = groupSubjects.Where(gc => gc.SubjectId == obj.SubjectId).ToList(); ;
                }

                _response.Result = _mapper.Map<IEnumerable<SubjectDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }

        // Get By Id Of Entity (Subject)
        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Subject obj = _db.Subjects.Find(id);
                _response.Result = _mapper.Map<SubjectDto>(obj);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }


        // Get By Name Of Entity (Subject)
        [HttpGet]
        [Route("GetByName/{name}")]
        public ResponseDto Get(string name)
        {
            try
            {
                Subject obj = _db.Subjects.FirstOrDefault(c => c.Name == name);
                _response.Result = _mapper.Map<SubjectDto>(obj);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }

        // Creating Entity (Subject)
        [HttpPost]
        public ResponseDto Post([FromBody] SubjectDto subjectDto)
        {
            try
            {
                Subject obj = _mapper.Map<Subject>(subjectDto);
                _db.Subjects.Add(obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<SubjectDto>(obj);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }

        // Updating Entity (Subject)
        [HttpPut]
        public ResponseDto Put([FromBody] SubjectDto subjectDto)
        {
            try
            {
                Subject obj = _mapper.Map<Subject>(subjectDto);
                _db.Subjects.Update(obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<SubjectDto>(obj);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }

        // Deleting Entity (Subject)
        [HttpDelete]
        [Route("{id:int}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Subject obj = _db.Subjects.Find(id);
                _db.Subjects.Remove(obj);
                _db.SaveChanges();

                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }
    }
}
