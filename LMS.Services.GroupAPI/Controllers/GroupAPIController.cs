using AutoMapper;
using LMS.Services.GroupAPI.Data;
using LMS.Services.GroupAPI.Models;
using LMS.Services.GroupAPI.Models.Dto;
using LMS.Services.GroupAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Services.GroupAPI.Controllers
{
    [Route("api/group")]
    [ApiController]
    public class GroupAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ISharedService _sharedService;
        private readonly ISubjectService _subjectService;
        private IMapper _mapper;
        private ResponseDto _response;
        public GroupAPIController(AppDbContext db,
            IMapper mapper,
            ISharedService sharedService,
            ISubjectService subjectService)
        {
            _db = db;
            _sharedService = sharedService;
            _mapper = mapper;
            _response = new ResponseDto();
            _subjectService = subjectService;
        }

        #region Get Group(s)

        // Get All Entities (Groups)
        [HttpGet]
        public async Task<ResponseDto> Get()
        {
            try
            {
                IEnumerable<Group> objList = _db.Groups.ToList();

                // getting groupSubjects from Shared Project
                IEnumerable<GroupSubjectDto> groupSubjects = await _sharedService.GetGroupSubjects();

                foreach (Group obj in objList)
                {
                    obj.GroupSubjects = groupSubjects.Where(gc => gc.GroupId == obj.GroupId).ToList();   
                }

                _response.Result = _mapper.Map<IEnumerable<GroupDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }

        // Get By Id Of Entity (Group)
        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Group obj = _db.Groups.Find(id);
                _response.Result = _mapper.Map<GroupDto>(obj);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }


        // Get By Name Of Entity (Group)
        [HttpGet]
        [Route("GetByName/{name}")]
        public ResponseDto Get(string name)
        {
            try
            {
                Group obj = _db.Groups.FirstOrDefault(c => c.Name == name);
                _response.Result = _mapper.Map<GroupDto>(obj);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }


        // getting students by group id
        [HttpGet]
        [Route("GetBySubjects/{id}")]
        public async Task<ResponseDto> GetSubjectByGroupId(int id)
        {
            try
            {
                // getting groupSubjects from Shared Project
                IEnumerable<GroupSubjectDto> groupSubjects = await _sharedService.GetGroupSubjects();

                // getting subject ids by group id from groupSubjects
                Group group = _db.Groups.Find(id);
                if(group == null)
                {
                    throw new Exception("Group Not Found");
                }

                group.GroupSubjects = new();


                // setting subjectId for group
                foreach(var groupSubject in groupSubjects)
                {
                    if(groupSubject.GroupId == id)
                    {
                        group.GroupSubjects.Add(new GroupSubjectDto()
                        {
                            SubjectId = groupSubject.SubjectId
                        });
                    }
                }


                #region Subjects for Sending group
                List<SubjectDto> sendSubjectList = new();

                // get all Subjects
                IEnumerable<SubjectDto> subjects = await _subjectService.GetSubjects();

                // setting subjects group.GroupSubject SubjectIds
                foreach(var groupSubject in group.GroupSubjects)
                {
                    foreach(var subject in subjects)
                    {
                        if (groupSubject.SubjectId == subject.SubjectId)
                        {
                            sendSubjectList.Add(subject);
                        }
                    }
                }    

                #endregion

                _response.Result = _mapper.Map<List<SubjectDto>>(sendSubjectList);  
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }

        #endregion

        // Creating Entity (Group)
        [HttpPost]
        public ResponseDto Post([FromBody] GroupDto groupDto)
        {
            try
            {
                Group obj = _mapper.Map<Group>(groupDto);
                _db.Groups.Add(obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<GroupDto>(obj);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }

        // Updating Entity (Group)
        [HttpPut]
        public ResponseDto Put([FromBody] GroupDto groupDto)
        {
            try
            {
                Group obj = _mapper.Map<Group>(groupDto);
                _db.Groups.Update(obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<GroupDto>(obj);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }

        // Deleting Entity (Group)
        [HttpDelete]
        [Route("{id:int}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Group obj = _db.Groups.Find(id);
                _db.Groups.Remove(obj);
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
