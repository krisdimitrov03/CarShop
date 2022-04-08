using Microsoft.AspNetCore.Mvc;

namespace CarShop.Areas.Emp.Controllers
{
    public class CarsController : BaseController
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}
