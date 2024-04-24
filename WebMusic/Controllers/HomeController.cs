using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebMusic.BLL.DTO;
using WebMusic.BLL.Interfaces;
using WebMusic.Filters;
using WebMusic.Models;

namespace WebMusic.Controllers
{
    [Culture]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMediaService _mediaService;
        private readonly IGenreService _genreService;
        private readonly IExecutorService _executorService;
        private readonly ILogger<HomeController> _logger;
        //private IEnumerable<MediaDTO> songs;

        public HomeController(IUserService userService, IMediaService mediaService, IGenreService genreService, IExecutorService executorService, ILogger<HomeController> logger)
        {
            _userService = userService;
            _mediaService = mediaService;
            _genreService = genreService;
            _executorService = executorService;
            _logger = logger;
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Index(int page = 1, SortState sortOrder = SortState.SongAsc)
        {
            int pageSize = 12;   
            IEnumerable<MediaDTO> mediaItems = await _mediaService.GetMedias();
            mediaItems = sortOrder switch
            {
                SortState.SongDesc => mediaItems.OrderByDescending(s => s.Title),
                SortState.ExecutorAsc => mediaItems.OrderBy(s => s.Executor),
                SortState.ExecutorDesc => mediaItems.OrderByDescending(s => s.Executor),
                
                _ => mediaItems.OrderBy(s => s.Title),
            };
            var count = mediaItems.Count();
            var items = mediaItems.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel(items, pageViewModel, new SortViewModel(sortOrder));
            //IEnumerable<IndexViewModel> viewModelList = mediaItems.Select(media => new IndexViewModel(songs, pageViewModel));
            return View(viewModel);
            //return View(await _mediaService.GetMedias());
        }

        public IActionResult Privacy()
        {
            HttpContext.Session.SetString("path", Request.Path);
            return View();
        }
        public ActionResult ChangeCulture(string lang)
        {
            string? returnUrl =  "/Home/Index";

            // Список культур
            List<string> cultures = new List<string>() { "ru", "en", "uk", "de"};
            if (!cultures.Contains(lang))
            {
                lang = "ru";
            }

            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(10); // срок хранения куки - 10 дней
            Response.Cookies.Append("lang", lang, option); // создание куки
            return Redirect(returnUrl);
        }

        public async Task<IActionResult> NavigationAsync()
        {
            var genres = await _genreService.GetGenres();
            var executors = await _executorService.GetExecutors();
            var songs = await _mediaService.GetMedias();

            GenresAndExecutorsViewModel viewModel = new GenresAndExecutorsViewModel(genres,executors,songs);
            return View(viewModel);
        }

        public async Task<IActionResult> GetSongsE(int id)
        {

            var songs = await _mediaService.GetingSongsByExecutor(id); 
           
            return View("SongsByFilter", songs);

        }
        public async Task<IActionResult> GetSongsG(int id)
        {
            var songs = await _mediaService.GetingSongsByGenre(id);
            return View("SongsByFilter", songs);
           
        }
        

    }
}
