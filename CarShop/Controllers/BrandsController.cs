using Microsoft.AspNetCore.Mvc;

namespace CarShop.Controllers
{
    public class BrandsController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
    }
}
