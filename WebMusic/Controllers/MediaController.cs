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
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MediaDTO new_media, IFormFile song)
        {
            UserDTO user = await _userService.GetUser(new_media.Id_User);
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



            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await song.CopyToAsync(fileStream);
            }


            MediaDTO media = new MediaDTO
            {
                Title = new_media.Title,
                id_Genre = genre.Id,
                id_Executor = executor.Id,
                Path = path,
                Id_User = user.Id

            };

            await _mediaService.CreateMedia(media);

            return View("~/Views/Home/Index.cshtml", await _mediaService.GetMedias());

        }
    }
};
