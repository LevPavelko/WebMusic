﻿using Microsoft.AspNetCore.Mvc;
using WebMusic.Models;
using WebMusic.BLL.DTO;
using System.Security.Cryptography;
using System.Text;
using WebMusic.BLL.Interfaces;
using WebMusic.DAL.Entities;
using Microsoft.AspNetCore.Http;
using WebMusic.Filters;

namespace WebMusic.Controllers
{
    [Culture]
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
                    ModelState.AddModelError("", "Не правильный логин или пароль");
                    return View("Index", loginModel); 
                }

                string? salt = users.Salt;
                int ?status = users.Status;

                byte[] password = Encoding.Unicode.GetBytes(salt + loginModel.Password);


                byte[] byteHash = SHA256.HashData(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                if (users.Password != hash.ToString())
                {
                    ModelState.AddModelError("", "Не правильный логин или пароль");
                    return View("Index",loginModel);
                }
                HttpContext.Session.SetString("Login", users.Login);
                HttpContext.Session.SetInt32("Status", status.Value);
                HttpContext.Session.SetInt32("Id", users.Id);



                return RedirectToAction("Index", "Home");
            }
            return View("Index", loginModel);
        }


       
    }
}
