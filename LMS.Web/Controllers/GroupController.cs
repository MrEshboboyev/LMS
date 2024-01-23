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

        public async Task<IActionResult> GroupIndex()
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

        [HttpGet]
        public IActionResult GroupCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GroupCreate(GroupDto groupDto)
        {
            List<GroupDto> groups = new();
            _response = await _groupService.CreateGroupAsync(groupDto);
            if (_response != null && _response.IsSuccess)
            {
                TempData["success"] = "Group created successfully!";
                return RedirectToAction("GroupIndex");
            }
            else
            {
                TempData["error"] = _response.Message;
            }

            return View(groups);
        }

        [HttpGet]
        public async Task<IActionResult> GroupDelete(int id)
        {
            GroupDto group = new();
            _response = await _groupService.GetGroupByIdAsync(id);
            if (_response != null && _response.IsSuccess)
            {
                group = JsonConvert.DeserializeObject<GroupDto>(Convert.ToString(_response.Result));
                return View(group);
            }
            else
            {
                TempData["error"] = _response.Message;
            }

            return View();
        }

        [HttpPost]
        [ActionName("GroupDelete")]
        public async Task<IActionResult> GroupDeletePOST(int id)
        {
            List<GroupDto> groups = new();
            _response = await _groupService.DeleteGroupAsync(id);
            if (_response != null && _response.IsSuccess)
            {
                TempData["success"] = "Group deleted successfully!";
                return RedirectToAction("GroupIndex");
            }
            else
            {
                TempData["error"] = _response.Message;
            }

            return View(groups);
        }

        [HttpGet]
        public async Task<IActionResult> GroupEdit(int id)
        {
            GroupDto group = new();
            _response = await _groupService.GetGroupByIdAsync(id);
            if (_response != null && _response.IsSuccess)
            {
                group = JsonConvert.DeserializeObject<GroupDto>(Convert.ToString(_response.Result));
                return View(group);
            }
            else
            {
                TempData["error"] = _response.Message;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GroupEdit(GroupDto groupDto)
        {
            List<GroupDto> groups = new();
            _response = await _groupService.UpdateGroupAsync(groupDto);
            if (_response != null && _response.IsSuccess)
            {
                TempData["success"] = "Group updated successfully!";
                return RedirectToAction("GroupIndex"); 
            }
            else
            {
                TempData["error"] = _response.Message;
            }

            return View(groups);
        }
    }
}
