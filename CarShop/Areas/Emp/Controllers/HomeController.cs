using Microsoft.AspNetCore.Mvc;

namespace CarShop.Areas.Emp.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
