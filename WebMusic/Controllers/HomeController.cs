using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebMusic.BLL.DTO;
using WebMusic.BLL.Interfaces;
using WebMusic.DAL.Entities;
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
        private readonly IFavSongsService _favSongsService;
        private readonly ILogger<HomeController> _logger;
        //private IEnumerable<MediaDTO> songs;

        public HomeController(IUserService userService, IMediaService mediaService, IGenreService genreService, IExecutorService executorService, 
            ILogger<HomeController> logger, IFavSongsService favSongsService)
        {
            _userService = userService;
            _mediaService = mediaService;
            _genreService = genreService;
            _executorService = executorService;
            _favSongsService = favSongsService;
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
        public async Task<IActionResult> AddFavSong(FavSongsDTO favSong)
        {
            var userId = HttpContext.Session.GetInt32("Id").Value;
            favSong.Id_User = userId;
            UserDTO user = await _userService.GetUser(userId);
            if (user == null)
            {
                return BadRequest("User not found.");

            }
         
            MediaDTO song = await _mediaService.GetMedia(favSong.Id);
            if (song == null)
            {
                return BadRequest("Genre not found.");
            }

            if (ModelState.IsValid)
            {
              
                FavSongsDTO favSongs = new FavSongsDTO
                {
                 
                    Id_Song = song.Id,
                    Id_User = user.Id

                };
                var all = await _favSongsService.GetAllItems();
                foreach (var item in all)
                {
                    if(item.Id_User == userId && item.Id_Song == song.Id)
                    {
                        string message = Resources.Resource.AlreadyInPlaylist;
                      
                        ViewBag.Message = message;
                        return View("IntermediatePage","Home");
                    }
                }
                await _favSongsService.AddSong(favSongs);

                
            }

            //var songs = await _favSongsService.GetSongsByUser(id);
            //return View("SongsByFilter", songs);
            string success = Resources.Resource.SuccessAddedToFav;
            ViewBag.Message = success;
            return View("IntermediatePage", "Home");
            
        }
        public IActionResult IntermediatePage()
        {
            return View();
        }

        public async Task<IActionResult> FavoriteSongs()
        {
            var userId = HttpContext.Session.GetInt32("Id").Value;
            var all = await _favSongsService.GetAllItems();

            
            List<MediaDTO> favoriteSongs = new List<MediaDTO>();
            foreach (var item in all)
            {
               if(item.Id_User == userId) 
               {
                    var song = await _mediaService.GetMedia(item.Id_Song);
                    favoriteSongs.Add(song);
               }
            }

            return View(favoriteSongs);

        }
        public async Task<IActionResult> DeleteSong(int id)
        {
            var userId = HttpContext.Session.GetInt32("Id").Value;
            var all = await _favSongsService.GetAllItems();
            foreach (var item in all)
            {
                if (item.Id_User == userId && item.Id_Song == id)
                {
                    var idItem = item.Id;
                    await _favSongsService.DeleteSong(idItem);
                    string message = Resources.Resource.SuccessDeleteFromFav;

                    ViewBag.Message = message;
                   
                }
            }
            return View("IntermediatePage", "Home");

        }
        

    }
}
