using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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



        public async Task<IActionResult> Index()
        {
            //if (HttpContext.Session.GetString("LastName") != null
            //   && HttpContext.Session.GetString("FirstName") != null)
            //    return View(await _mediaService.GetMedias());
            //else
            //    return RedirectToAction("Login", "LoginAndRegister");
            return View(await _mediaService.GetMedias());
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

    }
}
