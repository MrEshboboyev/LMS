using LMS.Web.Models;
using LMS.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LMS.Web.Controllers
{
    public class SubjectController : Controller
    {
        #region DI 
        // DI for ISubjectService
        private readonly ISubjectService _subjectService;
        private ResponseDto? _response;
        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
            _response = new ResponseDto();
        }
        #endregion

        #region Index Subject
        public async Task<IActionResult> SubjectIndex()
        {
            List<SubjectDto> subjects = new();  
            _response = await _subjectService.GetAllSubjectsAsync();
            if(_response != null && _response.IsSuccess)
            {
                subjects = JsonConvert.DeserializeObject<List<SubjectDto>>(Convert.ToString(_response.Result));
            }
            else
            {
                TempData["error"] = _response.Message;
            }

            return View(subjects);
        }
        #endregion

        #region Create Subject

        [HttpGet]
        public IActionResult SubjectCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubjectCreate(SubjectDto subjectDto)
        {
            List<SubjectDto> subjects = new();
            _response = await _subjectService.CreateSubjectAsync(subjectDto);
            if (_response != null && _response.IsSuccess)
            {
                TempData["success"] = "Subject created successfully!";
                return RedirectToAction("SubjectIndex");
            }
            else
            {
                TempData["error"] = _response.Message;
            }

            return View(subjects);
        }

        #endregion

        #region Delete Subject

        [HttpGet]
        public async Task<IActionResult> SubjectDelete(int id)
        {
            SubjectDto subject = new();
            _response = await _subjectService.GetSubjectByIdAsync(id);
            if (_response != null && _response.IsSuccess)
            {
                subject = JsonConvert.DeserializeObject<SubjectDto>(Convert.ToString(_response.Result));
                return View(subject);
            }
            else
            {
                TempData["error"] = _response.Message;
            }

            return View();
        }

        [HttpPost]
        [ActionName("SubjectDelete")]
        public async Task<IActionResult> SubjectDeletePOST(int id)
        {
            List<SubjectDto> subjects = new();
            _response = await _subjectService.DeleteSubjectAsync(id);
            if (_response != null && _response.IsSuccess)
            {
                TempData["success"] = "Subject deleted successfully!";
                return RedirectToAction("SubjectIndex");
            }
            else
            {
                TempData["error"] = _response.Message;
            }

            return View(subjects);
        }

        #endregion

        #region Update Subject

        [HttpGet]
        public async Task<IActionResult> SubjectEdit(int id)
        {
            SubjectDto subject = new();
            _response = await _subjectService.GetSubjectByIdAsync(id);
            if (_response != null && _response.IsSuccess)
            {
                subject = JsonConvert.DeserializeObject<SubjectDto>(Convert.ToString(_response.Result));
                return View(subject);
            }
            else
            {
                TempData["error"] = _response.Message;
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubjectEdit(SubjectDto subjectDto)
        {
            List<SubjectDto> subjects = new();
            _response = await _subjectService.UpdateSubjectAsync(subjectDto);
            if (_response != null && _response.IsSuccess)
            {
                TempData["success"] = "Subject updated successfully!";
                return RedirectToAction("SubjectIndex"); 
            }
            else
            {
                TempData["error"] = _response.Message;
            }

            return View(subjects);
        }

        #endregion

        #region All Groups Of Subject
        
        public async Task<IActionResult> SubjectGroups(int id)
        {
            List<GroupDto> groups = new();
            _response = await _subjectService.GetGroupsBySubjectIdAsync(id);
            if(_response != null && _response.IsSuccess)
            {
                groups = JsonConvert.DeserializeObject<List<GroupDto>>(Convert.ToString(_response.Result));
                ViewBag.Subject = await GetSubjectAsync(id);
                return View(groups);
            }
            else
            {
                TempData["error"] = _response.Message;
            }

            return View();
        }

        #endregion

        #region Private Tasks

        // Get Subject By Id
        private async Task<SubjectDto> GetSubjectAsync(int id)
        {
            SubjectDto subject = new();
            _response = await _subjectService.GetSubjectByIdAsync(id);
            if (_response != null && _response.IsSuccess)
            {
                subject = JsonConvert.DeserializeObject<SubjectDto>(Convert.ToString(_response.Result));
                return subject;
            }
            else
            {
                TempData["error"] = _response.Message;
            }

            return new SubjectDto();
        }

        #endregion
    }
}
