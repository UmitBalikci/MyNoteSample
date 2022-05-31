using Microsoft.AspNetCore.Mvc;
using MyNoteSample.Business;
using MyNoteSample.Models;
using MyNoteSample.Models.Entities;

namespace MyNoteSample.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;

        public CategoryController()
        {
            _categoryService = new CategoryService();
        }
        public IActionResult Index()
        {
            return View(_categoryService.List().Data);
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
        public IActionResult Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                ServiceResult<Category> result = _categoryService.Create(model, HttpContext);
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
