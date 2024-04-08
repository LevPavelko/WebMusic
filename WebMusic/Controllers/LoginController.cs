using Microsoft.AspNetCore.Mvc;
using WebMusic.Models;
using WebMusic.BLL.DTO;
using System.Security.Cryptography;
using System.Text;
using WebMusic.BLL.Interfaces;
using WebMusic.DAL.Entities;

namespace WebMusic.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                //string currentUser = Login == loginModel.Login;
              
                var users = await _userService.GetUserByLogin(loginModel.Login);
                if (users == null)
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(loginModel);
                }

                string? salt = users.Salt;


                byte[] password = Encoding.Unicode.GetBytes(salt + loginModel.Password);


                byte[] byteHash = SHA256.HashData(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                if (users.Password != hash.ToString())
                {
                    ModelState.AddModelError("", "Wrong login or password!");
                    return View(loginModel);
                }
                HttpContext.Session.SetString("Login", users.Login);
                return RedirectToAction("Index", "Home");
            }
            return View(loginModel);
        }


       
    }
}
