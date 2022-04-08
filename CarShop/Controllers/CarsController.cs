using CarShop.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarShop.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarService service;

        public CarsController(ICarService _service)
        {
            service = _service;
        }

        public async Task<IActionResult> All()
        {
            var cars = await service.GetAll();

            return View(cars);
        }

        public async Task<IActionResult> Details(string id)
        {
            var car = await service.GetCarDetails(id);

            return View(car);
        } 
    }
}
