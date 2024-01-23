using AutoMapper;
using LMS.Services.StudentAPI.Data;
using LMS.Services.StudentAPI.Models;
using LMS.Services.StudentAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Services.StudentAPI.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private AppDbContext _db;
        private IMapper _mapper;
        private ResponseDto _response;
        public StudentAPIController(AppDbContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
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
        public ResponseDto Get(int id)
        {
            try
            {
                Student obj = _db.Students.Find(id);
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
    }
}
