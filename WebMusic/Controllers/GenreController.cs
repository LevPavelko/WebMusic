using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMusic.BLL.DTO;
using WebMusic.BLL.Interfaces;
using WebMusic.BLL.Services;
using WebMusic.DAL.Entities;
using WebMusic.Filters;

namespace WebMusic.Controllers
{
    [Culture]
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
            if (ModelState.IsValid)
            {
                GenreDTO genre = new GenreDTO();
                //genre.Id = g.Id;
                genre.Name = g.Name;
                await _genreService.CreateGenre(genre);

                //return RedirectToAction("Index");

                return RedirectToAction("Index", "Home");
            }
            //return BadRequest(ModelState);
            return View("PostGenre", g);
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

            return RedirectToAction("Index", "Home");
            //return View("~/Views/Home/Index.cshtml", await _mediaService.GetMedias());
        }

        //[HttpPost,ActionName("EditGenre")]
        //[ValidateAntiForgeryToken]
        public async Task <IActionResult> Edit(GenreDTO g)
        {
            if (ModelState.IsValid)
            {
                await _genreService.UpdateGenre(g);


                return RedirectToAction("Index", "Home");
            }
            return View("EditGenre", g);


        }
        public async Task<IActionResult> EditGenre(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                GenreDTO team = await _genreService.GetGenre((int)id);
                return View(team);
            }
            catch 
            {
                return NotFound();
            }
        }
    }
}
