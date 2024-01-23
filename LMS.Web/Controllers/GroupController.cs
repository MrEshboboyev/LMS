using LMS.Web.Models;
using LMS.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LMS.Web.Controllers
{
    public class GroupController : Controller
    {
        // DI for IGroupService
        private readonly IGroupService _groupService;
        private ResponseDto? _response;
        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
            _response = new ResponseDto();
        }

        public async Task<IActionResult> Index()
        {
            List<GroupDto> groups = new();  
            _response = await _groupService.GetAllGroupsAsync();
            if(_response != null && _response.IsSuccess)
            {
                groups = JsonConvert.DeserializeObject<List<GroupDto>>(Convert.ToString(_response.Result));
            }
            else
            {
                TempData["error"] = _response.Message;
            }

            return View(groups);
        }
    }
}
