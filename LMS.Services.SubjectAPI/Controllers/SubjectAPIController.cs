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
        private readonly IGroupService _groupService;
        private IMapper _mapper;
        private ResponseDto _response;
        public SubjectAPIController(AppDbContext db,
            IMapper mapper,
            ISharedService sharedService,
            IGroupService groupService)
        {
            _db = db;
            _sharedService = sharedService;
            _mapper = mapper;
            _response = new ResponseDto();
            _groupService = groupService;
        }

        #region Get Subject(s)

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

        // Get Groups By Subject Id
        [HttpGet]
        [Route("GetGroupsBySubject/{id}")]
        public async Task<ResponseDto> GetGroupsBySubjectId(int id)
        {
            try
            {
                // getting groupSubjects from Shared Project
                IEnumerable<GroupSubjectDto> groupSubjects = await _sharedService.GetGroupSubjects();

                // getting group ids by group id from groupSubjects
                Subject subject = _db.Subjects.Find(id);
                if (subject == null)
                {
                    throw new Exception("Subject Not Found");
                }

                subject.GroupSubjects = new();


                // setting subjectId for group
                foreach (var groupSubject in groupSubjects)
                {
                    if (groupSubject.SubjectId == id)
                    {
                        subject.GroupSubjects.Add(new GroupSubjectDto()
                        {
                            GroupId = groupSubject.GroupId
                        });
                    }
                }


                #region Groups for Sending 
                List<GroupDto> sendGroupList = new();

                // get all Subjects
                IEnumerable<GroupDto> groups = await _groupService.GetGroupsAsync();

                // setting subjects group.GroupSubject SubjectIds
                foreach (var groupSubject in subject.GroupSubjects)
                {
                    foreach (var group in groups)
                    {
                        if (groupSubject.GroupId == group.GroupId)
                        {
                            sendGroupList.Add(group);
                        }
                    }
                }

                #endregion

                _response.Result = _mapper.Map<List<GroupDto>>(sendGroupList);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }

        #endregion

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
