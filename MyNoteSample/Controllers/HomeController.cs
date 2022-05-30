using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyNoteSample.Business;
using MyNoteSample.Core;
using MyNoteSample.Models;
using MyNoteSample.Models.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyNoteSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserService _userService;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _userService = new UserService(HttpContext);
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                ServiceResult<User> result = _userService.Login(model);
                if (!result.IsError)
                {
                    HttpContext.Session.SetInt32(Constants.UserId, result.Data.Id);
                    HttpContext.Session.SetString(Constants.UserName, result.Data.Username);
                    HttpContext.Session.SetString(Constants.UserEmail, result.Data.Email);
                    HttpContext.Session.SetString(Constants.UserRole, result.Data.IsAdmin ? "admin" : "member");
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }
            }
            return View(model);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ServiceResult<object> result = _userService.Register(model);
                if (!result.IsError)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }
            }
            return View(model);
        }   
        public IActionResult ShowProfile()
        {
            return View();
        }
        public IActionResult EditProfile()
        {
            return View();
        }
        public IActionResult DeleteProfile()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
