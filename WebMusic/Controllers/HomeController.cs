using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebMusic.BLL.Interfaces;
using WebMusic.Models;

namespace WebMusic.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMediaService _mediaService;
        private readonly IGenreService _genreService;
        private readonly IExecutorService _executorService;
        private readonly ILogger<HomeController> _logger;
        public HomeController(IUserService userService, IMediaService mediaService, IGenreService genreService, IExecutorService executorService, ILogger<HomeController> logger)
        {
            _userService = userService;
            _mediaService = mediaService;
            _genreService = genreService;
            _executorService = executorService;
            _logger = logger;
        }





        public async Task<IActionResult> Index()
        {
            return View(await _mediaService.GetMedias());
        }

        public IActionResult Privacy()
        {
            return View();
        }

       
    }
}
