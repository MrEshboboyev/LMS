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
        private readonly IStudentService _studentService;
        private ResponseDto? _response;
        public GroupController(IGroupService groupService, IStudentService studentService)
        {
            _groupService = groupService;
            _studentService = studentService;
            _response = new ResponseDto();
        }

        #region Index Group
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
        #endregion

        #region Create Group

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

        #endregion

        #region Delete Group

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

        #endregion

        #region Update Group

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

        #endregion

        #region Adding Student To Group

        [HttpGet]
        public async Task<IActionResult> GroupAddStudent(int id)
        {
            GroupDto group = new();
            _response = await _groupService.GetGroupByIdAsync(id);
            if (_response != null && _response.IsSuccess)
            {
                group = JsonConvert.DeserializeObject<GroupDto>(Convert.ToString(_response.Result));
                ViewBag.GroupName = group.Name;
                StudentDto studentDto = new()
                {
                    GroupId = id
                };
                return View(studentDto);
            }
            else
            {
                TempData["error"] = _response.Message;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GroupAddStudent(StudentDto studentDto)
        {
            _response = await _studentService.CreateStudentAsync(studentDto);
            if (_response != null && _response.IsSuccess)
            {
                TempData["success"] = "Added Student Successfully!";
                return RedirectToAction("GroupStudents", new { id = studentDto.GroupId });
            }
            else
            {
                TempData["error"] = _response.Message;
            }

            return View();
        }
        #endregion

        #region All Students Of Group

        [HttpGet]
        public async Task<IActionResult> GroupStudents(int id)
        {
            GroupDto group = new();
            _response = await _groupService.GetGroupByIdAsync(id);
            if(_response != null && _response.IsSuccess)
            {
                group = JsonConvert.DeserializeObject<GroupDto>(Convert.ToString(_response.Result));
                List<StudentDto> students = GetStudentByGroupName(group.Name);
                ViewBag.Group = group;
                return View(students);  
            }
            else
            {
                TempData["error"] = _response.Message;
            }
            ViewBag.GroupName = group.Name;

            return View();
        }

        #endregion

        #region Private Tasks
        
        private List<StudentDto> GetStudentByGroupName(string groupName)
        {
            List<StudentDto> students = new();
            _response = _studentService.GetStudentsByGroupNameAsync(groupName).GetAwaiter().GetResult();
            if(_response != null && _response.IsSuccess)
            {
                students = JsonConvert.DeserializeObject<List<StudentDto>>(Convert.ToString(_response.Result));
                return students;
            }
            else
            {
                TempData["error"] = _response.Message;
            }

            return new List<StudentDto>();
        }

        #endregion
    }
}
