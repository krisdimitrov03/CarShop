using Microsoft.AspNetCore.Mvc;

namespace CarShop.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Personal(string id)
        {
            return View();
        }
    }
}
