using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyNoteSample.Business;
using MyNoteSample.Models;
using MyNoteSample.Models.Entities;
using System.Collections.Generic;

namespace MyNoteSample.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController()
        {
            _userService = new UserService(HttpContext);
        }
        public IActionResult Index()
        {
            ServiceResult<List<User>> result = _userService.List();
            return View(result.Data);
        }
        public IActionResult Details()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ServiceResult<User> result = _userService.Create(model);
                if (!result.IsError)
                {
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
        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}
