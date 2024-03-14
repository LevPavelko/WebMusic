using Microsoft.AspNetCore.Mvc;
using WebMusic.Repository;
using WebMusic.Models;
namespace WebMusic.Controllers
{
    public class MediaController : Controller
    {
       
        IRepository repo;

        public MediaController(IRepository r)
        {
            repo = r;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var model = await repo.GetMediaList();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Media student)
        {
            if (ModelState.IsValid)
            {
                await repo.Create(student);
                await repo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }


    }
}
