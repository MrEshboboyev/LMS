using LMS.Web.Models;
using LMS.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace LMS.Web.Controllers
{
    public class StudentController : Controller
    {
        #region DI 
        // DI for IStudentService
        private readonly IStudentService _studentService;
        private readonly IGroupService _groupService;
        private ResponseDto? _response;
        public StudentController(IStudentService studentService, IGroupService groupService)
        {
            _studentService = studentService;
            _groupService = groupService;
            _response = new ResponseDto();
        }
        #endregion

        #region StudentIndex Action
        public async Task<IActionResult> StudentIndex()
        {
            List<StudentDto> students = new();
            _response = await _studentService.GetAllStudentsAsync();
            if (_response != null && _response.IsSuccess)
            {
                students = JsonConvert.DeserializeObject<List<StudentDto>>(Convert.ToString(_response.Result));
            }
            else
            {
                TempData["error"] = _response.Message;
            }

            return View(students);
        }
        #endregion

        #region Student Create Actions [Get, Post]
        [HttpGet]
        public async Task<IActionResult> StudentCreate()
        {
            await PrepareGroupsDropdown();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> StudentCreate(StudentDto studentDto)
        {
            _response = await _studentService.CreateStudentAsync(studentDto);
            if (_response != null && _response.IsSuccess)
            {
                TempData["success"] = "Student created successfully!";
                return RedirectToAction("StudentIndex");
            }
            else
            {
                TempData["error"] = _response.Message;
            }

            return View(studentDto);
        }

        #endregion

        #region Student Delete Actions [Get, Post]
        [HttpGet]
        public async Task<IActionResult> StudentDelete(int id)
        {
            StudentDto student = new();
            _response = await _studentService.GetStudentByIdAsync(id);
            if (_response != null && _response.IsSuccess)
            {
                student = JsonConvert.DeserializeObject<StudentDto>(Convert.ToString(_response.Result));
                return View(student);
            }
            else
            {
                TempData["error"] = _response.Message;
            }

            return View();
        }

        [HttpPost]
        [ActionName("StudentDelete")]
        public async Task<IActionResult> StudentDeletePOST(int id)
        {
            _response = await _studentService.DeleteStudentAsync(id);
            if (_response != null && _response.IsSuccess)
            {
                TempData["success"] = "Student deleted successfully!";
                return RedirectToAction("StudentIndex");
            }
            else
            {
                TempData["error"] = _response.Message;
            }

            return View();
        }
        #endregion

        #region Student Update Actions [Get, Post]
        [HttpGet]
        public async Task<IActionResult> StudentEdit(int id)
        {
            StudentDto student = new();
            _response = await _studentService.GetStudentByIdAsync(id);
            if (_response != null && _response.IsSuccess)
            {
                student = JsonConvert.DeserializeObject<StudentDto>(Convert.ToString(_response.Result));

                _response = await _groupService.GetAllGroupsAsync();
                if (_response != null && _response.IsSuccess)
                {
                    List<GroupDto> groups = JsonConvert.DeserializeObject<List<GroupDto>>(Convert.ToString(_response.Result));

                    List<SelectListItem> groupItems = new List<SelectListItem>();

                    foreach (var group in groups)
                    {
                        groupItems.Add(new() { Text = group.Name, Value = group.GroupId.ToString() });
                    }

                    ViewBag.Groups = groupItems;
                }
                return View(student);
            }
            else
            {
                TempData["error"] = _response.Message;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> StudentEdit(StudentDto studentDto)
        {
            _response = await _studentService.UpdateStudentAsync(studentDto);
            if (_response != null && _response.IsSuccess)
            {
                TempData["success"] = "Student updated successfully!";
                return RedirectToAction("StudentIndex");
            }
            else
            {
                TempData["error"] = _response.Message;
            }

            return View();
        }
        #endregion


        #region Students By Group (!Optimized)
        [HttpPost]
        public async Task<IActionResult> StudentsByGroupName(string groupName)
        {
            List<StudentDto> students = new();
            if(string.IsNullOrEmpty(groupName))
            {
                _response = await _studentService.GetAllStudentsAsync();
                if (_response != null && _response.IsSuccess)
                {
                    students = JsonConvert.DeserializeObject<List<StudentDto>>(Convert.ToString(_response.Result));
                }
                else
                {
                    TempData["error"] = _response.Message;
                }
            }
            else
            {

                _response = await _studentService.GetStudentsByGroupNameAsync(groupName);
                if (_response != null && _response.IsSuccess)
                {
                    students = JsonConvert.DeserializeObject<List<StudentDto>>(Convert.ToString(_response.Result));
                }
                else
                {
                    TempData["error"] = "Not Found";
                }
            }
            ViewData["GroupName"] = groupName;

            return View("StudentIndex", students);
        }
        #endregion

        #region Private Tasks
        private async Task PrepareGroupsDropdown()
        {
            _response = await _groupService.GetAllGroupsAsync();

            if (_response != null && _response.IsSuccess)
            {
                List<GroupDto> groups = JsonConvert.DeserializeObject<List<GroupDto>>(Convert.ToString(_response.Result));

                List<SelectListItem> groupItems = groups
                    .Select(group => new SelectListItem { Text = group.Name, Value = group.GroupId.ToString() })
                    .ToList();

                ViewBag.Groups = groupItems;
            }
            else
            {
                TempData["error"] = _response.Message;
            }
        }
        #endregion
    }
}
