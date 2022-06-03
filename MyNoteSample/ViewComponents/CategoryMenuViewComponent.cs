using Microsoft.AspNetCore.Mvc;
using MyNoteSample.Business;
using System.Threading.Tasks;

namespace MyNoteSample.ViewComponents
{
   //[ViewComponent(Name ="categories-menu-list")]
    public class CategoryMenuViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            CategoryService categoryService = new CategoryService();
            return View(categoryService.List().Data);
        }
    }
}
