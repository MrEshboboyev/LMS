using AutoMapper;
using LMS.Services.Shared.Data;
using LMS.Services.Shared.Models;
using LMS.Services.Shared.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Services.Shared.Controllers
{
    [Route("api/shared")]
    [ApiController]
    public class SharedAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private ResponseDto _response;
        public SharedAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
        }


        [HttpGet]
        public async Task<ResponseDto> Get()
        {
            try
            {
                IEnumerable<GroupSubject> objList = _db.GroupSubjects.ToList();

                _response.Result = _mapper.Map<IEnumerable<GroupSubjectDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }
    }
}
