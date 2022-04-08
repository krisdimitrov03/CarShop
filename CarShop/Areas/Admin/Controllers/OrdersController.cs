using Microsoft.AspNetCore.Mvc;

namespace CarShop.Areas.Admin.Controllers
{
    public class OrdersController : BaseController
    {
        public IActionResult All()
        {
            return View();
        }

        public IActionResult ForPerson(string id)
        {
            return View();
        }
    }
}
