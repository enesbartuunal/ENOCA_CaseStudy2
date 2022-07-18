using AspNetCoreHero.ToastNotification.Abstractions;
using ENOCA_CaseStudy.Models;
using ENOCA_CaseStudy.Services.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENOCA_CaseStudy.Controllers
{
    [Authorize]
    public class MovieController : Controller
    {
        private readonly MovieManager _movieManager;
        private readonly SaloonManager _saloonManager;
        private readonly INotyfService _notyf;
        public MovieController(MovieManager MovieManager, SaloonManager saloonManager, INotyfService notyf)
        {
            _movieManager = MovieManager;
            _saloonManager = saloonManager;
            _notyf = notyf;
        }
        // GET: MovieConroller
        [HttpGet("/Movie")]
        public async Task<ActionResult> Index(string startDate = "0", string overDate = "2022", int saloonId = 0)
        {
            try
            {
                ViewBag.SaloonList = _saloonManager.Get().Result.ToList();
                var sD = Convert.ToInt32(startDate);
                if (overDate == null)
                    overDate = "2022";
                var oD = Convert.ToInt32(overDate);
                var list = await _movieManager.Get(x => x.ProductionYear < oD && x.ProductionYear > sD);
                if (saloonId != 0)
                {
                    var list_2 = await _movieManager.GetBySaloonId(saloonId);

                    return View(list_2);

                }
                return View(list);

            }
            catch (Exception e)
            {

                return View(e);
            }

        }
        public async Task<ActionResult> ShowReleaseHistory(int id)
        {
            try
            {
                var list = await _movieManager.GetReleaseHistory(id);
                return View(list);

            }
            catch (Exception e)
            {

                return View(e);
            }

        }


        // GET: MovieConroller/Create
        public async Task<ActionResult> Create()
        {
            try
            {
                var MovieDto = new MovieDto();

                return View(MovieDto);
            }
            catch (Exception e)
            {

                return View(e);
            }
            return View();
        }

        // POST: MovieConroller/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MovieDto MovieDto, int[] saloonIds)
        {
            try
            {
                var movieList = _saloonManager.Get().Result.ToList();
                var saloonList = movieList.Where(x => saloonIds.Any(y => y == x.SaloonID)).ToList();
                var result = await _movieManager.Add(MovieDto);
                _notyf.Success("Film kayıt edildi.");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _notyf.Error("Film Kayıt edilirken hata oluştu.");
                return View(MovieDto);

            }
        }
        public async Task<ActionResult> CreateReleaseYear(int id)
        {
            try
            {
                ViewData["SaloonList"] = _saloonManager.Get().Result.ToList();
                var Movie = await _movieManager.GetById(id);

                return View(Movie);
            }
            catch (Exception e)
            {

                return View(e);
            }
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateReleaseYear(MovieDto MovieDto, int[] saloonIds, int releaseYear)
        {
            try
            {
                ViewData["SaloonList"] = _saloonManager.Get().Result.ToList();
                var movieList = _saloonManager.Get().Result.ToList();
                var saloonList = movieList.Where(x => saloonIds.Any(y => y == x.SaloonID)).ToList();
                await _movieManager.AddManyToManyRelation(saloonList, MovieDto, releaseYear);
                _notyf.Success("Gösterim Oluşturuldu!");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _notyf.Error("Gösterim Oluşturulurken hata oluştu.");
                return View(MovieDto);

            }
        }


        // GET: MovieConroller/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {

                var Movie = await _movieManager.GetById(id);

                return View(Movie);
            }
            catch (Exception e)
            {
                _notyf.Warning("Bilgiler getirilirken hata oluştu.");
                return View(e);
            }
        }

        // POST: MovieConroller/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MovieDto MovieDto)
        {
            try
            {
                _notyf.Success("Bilgiler güncellendi");
                await _movieManager.Update(MovieDto);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _notyf.Error("Bilgiler güncellenirken hata oluştu.");
                return View(MovieDto);
            }
        }

        // POST: MovieConroller/Delete/5
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {

                var result = await _movieManager.Delete(id);
                return Json(result);
            }
            catch (Exception e)
            {

                return Json(e);
            }
        }
    }
}
