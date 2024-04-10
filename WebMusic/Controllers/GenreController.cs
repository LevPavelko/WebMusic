using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMusic.BLL.DTO;
using WebMusic.BLL.Interfaces;
using WebMusic.BLL.Services;
using WebMusic.DAL.Entities;

namespace WebMusic.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;
        private readonly IMediaService _mediaService;
        public GenreController( IGenreService genreService, IMediaService mediaService)
        {
            _mediaService = mediaService;
            _genreService = genreService;
            
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> PostGenreAsync()
        {

            return View();


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> PostGenre(GenreDTO g)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            GenreDTO genre = new GenreDTO();
            //genre.Id = g.Id;
            genre.Name = g.Name;
            await _genreService.CreateGenre(genre);

            //return RedirectToAction("Index");

            return View("~/Views/Home/Index.cshtml", await _mediaService.GetMedias());
        }
        public async Task <IActionResult> AllGenres()
        {
            return View(await _genreService.GetGenres());
        }
        public async Task<IActionResult> DeleteGenre(int genreId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           

            await _genreService.DeleteGenre(genreId);


            return View("~/Views/Home/Index.cshtml", await _mediaService.GetMedias());
        }
    }
}
