using AutoMapper;
using LMS.Services.StudentAPI.Data;
using LMS.Services.StudentAPI.Models;
using LMS.Services.StudentAPI.Models.Dto;
using LMS.Services.StudentAPI.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Services.StudentAPI.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private AppDbContext _db;
        private readonly IGroupService _groupService;
        private IMapper _mapper;
        private ResponseDto _response;
        public StudentAPIController(AppDbContext db,
            IMapper mapper,
            IGroupService groupService)
        {
            _db = db;
            _mapper = mapper;
            _groupService = groupService;
            _response = new ResponseDto();
        }

        // Get All Entities (Students)
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Student> objList = _db.Students.ToList();
                _response.Result = _mapper.Map<IEnumerable<StudentDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }

        // Get By Id Of Entity (Student)
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ResponseDto> Get(int id)
        {
            try
            {
                Student obj = _db.Students.Find(id);

                IEnumerable<GroupDto> groups = await _groupService.GetGroupsAsync();

                obj.Group = groups.FirstOrDefault(gr => gr.GroupId == obj.GroupId);

                _response.Result = _mapper.Map<StudentDto>(obj);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }


        // Get By Name Of Entity (Student)
        [HttpGet]
        [Route("GetByName/{name}")]
        public ResponseDto Get(string name)
        {
            try
            {
                Student obj = _db.Students.FirstOrDefault(c => c.Name == name);
                _response.Result = _mapper.Map<StudentDto>(obj);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }

        // Creating Entity (Student)
        [HttpPost]
        public ResponseDto Post([FromBody] StudentDto studentDto)
        {
            try
            {
                Student obj = _mapper.Map<Student>(studentDto);
                _db.Students.Add(obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<StudentDto>(obj);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }

        // Updating Entity (Student)
        [HttpPut]
        public ResponseDto Put([FromBody] StudentDto studentDto)
        {
            try
            {
                Student obj = _mapper.Map<Student>(studentDto);
                _db.Students.Update(obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<StudentDto>(obj);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }

        // Deleting Entity (Student)
        [HttpDelete]
        [Route("{id:int}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Student obj = _db.Students.Find(id);
                _db.Students.Remove(obj);
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

        // Get Students by groupId
        [HttpGet]
        [Route("GetStudentsByGroupId/{groupId}")]
        public ResponseDto GetStudentsByGroupId(int groupId)
        {
            try
            {
                IEnumerable<Student> students = _db.Students.Where(std => std.GroupId == groupId).ToList();

                _response.Result = students;
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }

            return _response;
        }

        // Get Students by groupName
        [HttpGet]
        [Route("GetStudentsByGroupName/{groupName}")]
        public async Task<ResponseDto> GetStudentsByGroupName(string groupName)
        {
            try
            {
                IEnumerable<GroupDto> groups = await _groupService.GetGroupsAsync();

                // find group
                GroupDto group = groups.FirstOrDefault(gr => gr.Name == groupName);

                IEnumerable<Student> students = _db.Students.Where(std => std.GroupId == group.GroupId).ToList();

                foreach(var std in  students)
                {
                    std.Group = group;
                }

                _response.Result = students;
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
