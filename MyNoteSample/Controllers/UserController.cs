﻿using Microsoft.AspNetCore.Http;
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
            _userService = new UserService();
        }
        public IActionResult Index()
        {
            ServiceResult<List<User>> result = _userService.List();
            return View(result.Data);
        }
        public IActionResult Details(int id)
        {
            ServiceResult<User> result = _userService.Find(id);

            if (result.Data == null)
            {
                return RedirectToAction("Index");
            }
            return View(result.Data);
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
                ServiceResult<User> result = _userService.Create(model, HttpContext);
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
        public IActionResult Edit(int id)
        {
            ServiceResult<User> result = _userService.Find(id);

            if (result.Data == null)
            {
                return RedirectToAction("Index");
            }

            UserEditViewModel model = new UserEditViewModel
            {
                FullName = result.Data.FullName,
                Username = result.Data.Username,
                Email = result.Data.Email,
                IsActive = result.Data.IsActive,
                IsAdmin = result.Data.IsAdmin
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                ServiceResult<User> result = _userService.Update(id, model, HttpContext);
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
        public IActionResult Delete(int id)
        {
            ServiceResult<User> result = _userService.Find(id);

            if (result.Data == null)
            {
                return RedirectToAction("Index");
            }
            return View(result.Data);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            ServiceResult<object> result = _userService.Remove(id);
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

                return View(_userService.Find(id).Data);
            }
        }
    }
}
