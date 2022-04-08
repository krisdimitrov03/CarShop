using Microsoft.AspNetCore.Mvc;

namespace CarShop.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
