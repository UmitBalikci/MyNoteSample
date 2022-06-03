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
        public IActionResult Details(int id)
        {
            ServiceResult<Category> result = _categoryService.Find(id);

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
        public IActionResult Edit(int id)
        {
            ServiceResult<Category> result = _categoryService.Find(id);

            if (result.Data == null)
            {
                return RedirectToAction("Index");
            }

            CategoryViewModel model = new CategoryViewModel
            {
                Name = result.Data.Name,
                Description = result.Data.Description
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(int id, CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                ServiceResult<Category> result = _categoryService.Update(id, model, HttpContext);
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
            ServiceResult<Category> result = _categoryService.Find(id);

            if (result.Data == null)
            {
                return RedirectToAction("Index");
            }
            return View(result.Data);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            ServiceResult<object> result = _categoryService.Remove(id);
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

                return View(_categoryService.Find(id).Data);
            }
        }
    }
}
