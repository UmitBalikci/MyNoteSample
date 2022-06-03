using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyNoteSample.Business;
using MyNoteSample.Models;
using MyNoteSample.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MyNoteSample.Controllers
{
    public class NoteController : Controller
    {
        private readonly CategoryService _categoryService;
        private readonly NoteService _noteService;

        public NoteController()
        {
            _categoryService = new CategoryService();
            _noteService = new NoteService();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LikedList()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }
        public IActionResult Create()
        {
            LoadCategorySelectListDataToViewData();
            return View();
        }

        private void LoadCategorySelectListDataToViewData()
        {
            List<Category> categories = _categoryService.List().Data;
            List<SelectListItem> selectListItems =
                categories.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();

            ViewBag.categories = selectListItems;
        }

        [HttpPost]
        public IActionResult Create(NoteViewModel model)
        {
            if (ModelState.IsValid)
            {
                ServiceResult<Note> result = _noteService.Create(model, HttpContext);
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

            LoadCategorySelectListDataToViewData();

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
