using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMusic.BLL.DTO;
using WebMusic.BLL.Interfaces;
using WebMusic.DAL.Entities;
using WebMusic.Models;
namespace WebMusic.Controllers
{
    public class MediaController : Controller
    {
        IWebHostEnvironment _appEnvironment;
        private readonly IUserService _userService;
        private readonly IMediaService _mediaService;
        private readonly IGenreService _genreService;
        private readonly IExecutorService _executorService;
        public MediaController(IUserService userService, IMediaService mediaService, IGenreService genreService, IExecutorService executorService, IWebHostEnvironment appEnvironment)
        {
            _userService = userService;
            _mediaService = mediaService;
            _genreService = genreService;
            _executorService = executorService;
            _appEnvironment = appEnvironment;
        }


        //public IActionResult Index()
        //{
        //    return View();
        //}


        public async Task<IActionResult> CreateMediaAsync()
        {
            ViewBag.Genres = await _genreService.GetGenres();
            ViewBag.Executors = await _executorService.GetExecutors();
            return View();


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MediaDTO new_media, IFormFile song)
        {
          
            var userId = HttpContext.Session.GetInt32("Id").Value;
            new_media.Id_User = userId;
            UserDTO user = await _userService.GetUser(userId);
            if (user == null)
            {
                return BadRequest("User not found.");

            }
            ExecutorDTO executor = await _executorService.GetExecutorByName(new_media.Executor);
            if (executor == null)
            {
                return BadRequest("Executor not found.");
            }
            GenreDTO genre = await _genreService.GetGenreByName(new_media.Genre);
            if (genre == null)
            {
                return BadRequest("Genre not found.");
            }

            //string fileName = Path.GetFileName(song.FileName);
            //string path = Path.Combine(_appEnvironment.WebRootPath, "songs", fileName);
            string path = "/songs/" + song.FileName;
            new_media.Path = path;

           if(ModelState.IsValid)
           {
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await song.CopyToAsync(fileStream);
                }


                MediaDTO media = new MediaDTO
                {
                    Title = new_media.Title,
                    id_Genre = genre.Id,
                    id_Executor = executor.Id,
                    Path = new_media.Path,
                    Id_User = user.Id

                };

                await _mediaService.CreateMedia(media);

                return View("~/Views/Home/Index.cshtml", await _mediaService.GetMedias());
           }
            return View("CreateMedia", new_media);


           

        }

        public async Task<IActionResult> DeleteSong(int songId)
        {
            if (ModelState.IsValid)
            {
                await _mediaService.DeleteMedia(songId);
                return View("~/Views/Home/Index.cshtml", await _mediaService.GetMedias());
               
            }
            return BadRequest(ModelState);
        }
        public async Task<IActionResult> Edit(MediaDTO m, IFormFile song)
        {
            //if (ModelState.IsValid)
            //{
                UserDTO user = await _userService.GetUser(m.Id_User);
                if (user == null)
                {
                    return BadRequest("User not found.");

                }
                ExecutorDTO executor = await _executorService.GetExecutorByName(m.Executor);
                if (executor == null)
                {
                    return BadRequest("Executor not found.");
                }
                GenreDTO genre = await _genreService.GetGenreByName(m.Genre);
                if (genre == null)
                {
                    return BadRequest("Genre not found.");
                }
                string path = "/songs/" + song.FileName;

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await song.CopyToAsync(fileStream);
                }
            
                m.Id_User = user.Id;
                m.id_Executor = executor.Id;
                m.id_Genre = genre.Id;
                m.Path = path;
                await _mediaService.UpdateMedia(m);
                return RedirectToAction("Index", "Home", await _mediaService.GetMedias());

            //}
            //return BadRequest(ModelState);

        }
        public async Task<IActionResult> EditMedia(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                MediaDTO song = await _mediaService.GetMedia((int)id);
                return View(song);
            }
            catch
            {
                return NotFound();
            }
        }
    } 
};
