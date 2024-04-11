using Microsoft.AspNetCore.Mvc;
using WebMusic.BLL.DTO;
using WebMusic.BLL.Interfaces;
using WebMusic.BLL.Services;

namespace WebMusic.Controllers
{
    public class ExecutorController : Controller
    {
        private readonly IExecutorService _executorService;
        private readonly IMediaService _mediaService;
        public ExecutorController(IExecutorService executorService, IMediaService mediaService)
        {
            _mediaService = mediaService;
            _executorService = executorService;

        }
        
        public async Task<IActionResult> AllExecutors()
        {
            return View(await _executorService.GetExecutors());
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddExecutorAsync()
        {

            return View();


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddExecutor(ExecutorDTO e)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ExecutorDTO executor = new ExecutorDTO();
            //genre.Id = g.Id;
            executor.Name = e.Name;
            await _executorService.CreateExecutor(executor);

            //return RedirectToAction("Index");

            return View("~/Views/Home/Index.cshtml", await _mediaService.GetMedias());
        }
    }
}
