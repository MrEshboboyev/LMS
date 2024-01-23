using AutoMapper;
using LMS.Services.GroupAPI.Data;
using LMS.Services.GroupAPI.Models;
using LMS.Services.GroupAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Services.GroupAPI.Controllers
{
    [Route("api/group")]
    [ApiController]
    public class GroupAPIController : ControllerBase
    {
        private AppDbContext _db;
        private IMapper _mapper;
        private ResponseDto _response;
        public GroupAPIController(AppDbContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        // Get All Entities (Groups)
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Group> objList = _db.Groups.ToList();
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
