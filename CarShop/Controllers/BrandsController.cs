using CarShop.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarShop.Controllers
{
    public class BrandsController : Controller
    {
        private readonly IBrandService service;

        public BrandsController(IBrandService _service)
        {
            service = _service;
        }

        public async Task<IActionResult> All()
        {
            var brands = await service.GetBrands();

            return View(brands);
        }
    }
}
