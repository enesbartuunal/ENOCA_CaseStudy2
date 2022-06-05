using AspNetCoreHero.ToastNotification.Abstractions;
using ENOCA_CaseStudy.Models;
using ENOCA_CaseStudy.Services.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENOCA_CaseStudy.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly StudentManager _studentManager;
        private readonly FacultyManager _facultyManager;
        private readonly SectionManager _sectionManager;
        private readonly INotyfService _notyf;
        public StudentController(StudentManager studentManager, FacultyManager facultyManager, SectionManager sectionManager, INotyfService notyf)
        {
            _studentManager = studentManager;
            _facultyManager = facultyManager;
            _sectionManager = sectionManager;
            _notyf = notyf;
        }
        // GET: StudentConroller
        [HttpGet("/Student")]
        public async Task<ActionResult> Index()
        {
            try
            {

                var list = await _studentManager.Get();
                foreach (var item in list)
                {
                    item.FacultyName = _facultyManager.GetById(item.FacultyID).Result.FacultyName;
                }
                return View(list);
            }
            catch (Exception e)
            {

                return View(e);
            }

        }

        // GET: StudentConroller/Create
        public async Task<ActionResult> Create()
        {
            try
            {
                ViewData["FacultyList"] = _facultyManager.Get().Result.ToList();
                ViewBag.SectionNames = _sectionManager.Get().Result.ToList();
                var studentDto = new StudentDto();

                return View(studentDto);
            }
            catch (Exception e)
            {

                return View(e);
            }
            return View();
        }

        // POST: StudentConroller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StudentDto studentDto)
        {
            try
            {

                ViewData["FacultyList"] = _facultyManager.Get().Result.ToList();
                ViewBag.SectionNames = _sectionManager.Get().Result.ToList();
                var faculty = await _facultyManager.GetById(studentDto.FacultyID);
                studentDto.FacultyName = faculty.FacultyName;
                await _studentManager.Add(studentDto);
                _notyf.Success("Öğrenci kayıt edildi.");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _notyf.Error("Öğrenci Kayıt edilirken hata oluştu.");
                return View(studentDto);

            }
        }

        // GET: StudentConroller/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {

                var student = await _studentManager.GetById(id);
              
                return View(student);
            }
            catch (Exception e)
            {
                _notyf.Warning("Bilgiler getirilirken hata oluştu.");
                return View(e);
            }
        }

        // POST: StudentConroller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(StudentDto studentDto)
        {
            try
            {
                _notyf.Success("Bilgiler güncellendi");
                await _studentManager.Update(studentDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _notyf.Error("Öğrenci bilgileri güncellenirken hata oluştu.");
                return View(studentDto);
            }
        }

        // POST: StudentConroller/Delete/5
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {

                var result = await _studentManager.Delete(id);
                return Json(result);
            }
            catch (Exception e)
            {

                return Json(e);
            }
        }
    }
}
