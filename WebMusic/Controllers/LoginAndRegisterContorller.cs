using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMusic.Models;
using WebMusic.BLL.Interfaces;

//namespace WebMusic.Controllers
//{
//    public class LoginAndRegisterContorller : Controller
//    {
//        private readonly IUserService userService;
      
//        public LoginAndRegisterContorller(IUserService userserv)
//        {

//            userService = userserv;
//        }
//        public IActionResult Index()
//        {
//            return View();
//        }
//        public ActionResult Login()
//        {

//            return View();
//        }


//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> LoginAsync(LoginModel loginModel)
//        {
//            if (ModelState.IsValid)
//            {

//                var users = await userService.FirstOrDefaultAsync(a => a.Login == loginModel);
//                if (users == null)
//                {
//                    ModelState.AddModelError("", "Wrong login or password!");
//                    return View(loginModel);
//                }

//                string? salt = users.Salt;


//                byte[] password = Encoding.Unicode.GetBytes(salt + loginModel.Password);


//                byte[] byteHash = SHA256.HashData(password);

//                StringBuilder hash = new StringBuilder(byteHash.Length);
//                for (int i = 0; i < byteHash.Length; i++)
//                    hash.Append(string.Format("{0:X2}", byteHash[i]));

//                if (users.Password != hash.ToString())
//                {
//                    ModelState.AddModelError("", "Wrong login or password!");
//                    return View(loginModel);
//                }
//                HttpContext.Session.SetString("FirstName", users.FirstName);
//                HttpContext.Session.SetString("LastName", users.LastName);
//                return RedirectToAction("Index", "Home");
//            }
//            return View(loginModel);
//        }


//        public IActionResult Register()
//        {
//            return View();
//        }
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Register(RegisterModel reg)
//        {
//            if (ModelState.IsValid)
//            {
//                User user = new User();
//                user.FirstName = reg.FirstName;
//                user.LastName = reg.LastName;
//                user.Login = reg.Login;

//                byte[] saltbuf = new byte[16];

//                RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
//                randomNumberGenerator.GetBytes(saltbuf);

//                StringBuilder sb = new StringBuilder(16);
//                for (int i = 0; i < 16; i++)
//                    sb.Append(string.Format("{0:X2}", saltbuf[i]));
//                string salt = sb.ToString();


//                byte[] password = Encoding.Unicode.GetBytes(salt + reg.Password);


//                byte[] byteHash = SHA256.HashData(password);

//                StringBuilder hash = new StringBuilder(byteHash.Length);
//                for (int i = 0; i < byteHash.Length; i++)
//                    hash.Append(string.Format("{0:X2}", byteHash[i]));

//                user.Password = hash.ToString();
//                user.Salt = salt;
//                repo.CreateUser(user);
//                repo.Save();
//                return RedirectToAction("Login");
//            }

//            return View(reg);
//        }
//    }
//}
