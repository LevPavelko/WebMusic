using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMusic.BLL.Interfaces;
using WebMusic.BLL.DTO;
using WebMusic.DAL.Entities;
using WebMusic.Models;
using WebMusic.DAL.Repositories;
using WebMusic.Filters;
using WebMusic.BLL.Services;


namespace WebMusic.Controllers
{
    [Culture]
    public class RegisterController : Controller
    {
        private readonly IUserService _userService;

        public RegisterController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel reg)
        {
            if (ModelState.IsValid)
            {
                UserDTO user = new UserDTO();
                if (reg.Login == "admin")
                {
                    ModelState.AddModelError("Login", "admin - запрещенное имя");
                    return View("Index", reg);
                }
                UserDTO userLogin = await _userService.GetUserByLogin(reg.Login);
                if(reg.Login == userLogin.Login)
                {
                    ModelState.AddModelError("Login", "Пользователь с таким логином существует");
                    return View("Index", reg);
                }
                UserDTO userEmail = await _userService.GetUserByEmail(reg.Email);
                if (reg.Email == userEmail.Email)
                {
                    ModelState.AddModelError("Email", "Пользователь с таким email уже существует");
                    return View("Index", reg);
                }
                if (reg.Password.Length < 8)
                {
                    ModelState.AddModelError("Password", "Пароль должен содержать 8 символов");
                    return View("Index", reg);
                }
                else if (!reg.Password.Any(char.IsDigit))
                {
                    ModelState.AddModelError("Password", "Пароль должен содержать хотя бы одну цифру");
                    return View("Index", reg);
                }
                user.FirstName = reg.FirstName;
                user.LastName = reg.LastName;
                user.Login = reg.Login;
                user.Email = reg.Email;
                user.Status = 1;

                byte[] saltbuf = new byte[16];

                RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
                randomNumberGenerator.GetBytes(saltbuf);

                StringBuilder sb = new StringBuilder(16);
                for (int i = 0; i < 16; i++)
                    sb.Append(string.Format("{0:X2}", saltbuf[i]));
                string salt = sb.ToString();

                //переводим пароль в байт-массив  
                byte[] password = Encoding.Unicode.GetBytes(salt + reg.Password);

                //вычисляем хеш-представление в байтах  
                byte[] byteHash = SHA256.HashData(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                user.Password = hash.ToString();
                user.Salt = salt;
                await _userService.CreateUser(user);

                //return RedirectToAction("Login");// do something with that
               
                HttpContext.Session.SetString("Login", user.Login);
                HttpContext.Session.SetInt32("Status", user.Status.Value);
                HttpContext.Session.SetInt32("Id", user.Id);
                return RedirectToAction("Index", "Home");
            }

            return View("Index", reg);

        }
    }
}
