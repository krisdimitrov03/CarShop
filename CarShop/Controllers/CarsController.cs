using CarShop.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarShop.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarService service;

        public CarsController(ICarService _service)
        {
            service = _service;
        }

        public async Task<IActionResult> All(
            string? releaseYearMin=null, 
            string? releaseYearMax = null, 
            string? horsepowerMin = null, 
            string? horsepowerMax = null, 
            string? fuelType = null, 
            string? coupeType = null, 
            string? doorConfig = null,
            string? priceMin = null,
            string? priceMax = null)
        {
            var data = await service.GetFilterData();

            ViewBag.FuelTypes = data.FuelTypes
                .Select(n => new SelectListItem()
                {
                    Value = n.Id.ToString(),
                    Text = n.Name
                }).ToList();

            ViewBag.CoupeTypes = data.CoupeTypes
                .Select(n => new SelectListItem()
                {
                    Value = n.Id.ToString(),
                    Text = n.Name
                }).ToList();

            ViewBag.DoorConfigs = data.DoorConfigs
                .Select(n => new SelectListItem()
                {
                    Value = n.Id.ToString(),
                    Text = n.Name
                }).ToList();

            var cars = await service.All();

            int minRY = 0;
            int maxRY = 0;
            if(releaseYearMin != null && int.TryParse(releaseYearMin, out minRY))
			{
                cars = cars
                    .Where(c => int.Parse(c.ReleaseYear) >= minRY)
                    .ToList();
			}
            if(releaseYearMax != null && int.TryParse(releaseYearMax, out maxRY))
            {
                cars = cars
                    .Where(c => int.Parse(c.ReleaseYear) <= maxRY)
                    .ToList();
            }

            int minHP = 0;
            int maxHP = 0;
            if (horsepowerMin != null && int.TryParse(horsepowerMin, out minHP))
            {
                cars = cars
                    .Where(c => int.Parse(c.HorsePower) >= minHP)
                    .ToList();
            }
            if(horsepowerMax != null && int.TryParse(horsepowerMax, out maxHP))
            {
                cars = cars
                    .Where(c => int.Parse(c.HorsePower) <= maxHP)
                    .ToList();
            }
            if(fuelType != null)
            {
                cars = cars
                    .Where(c => c.FuelType == fuelType)
                    .ToList();
            }
            if (coupeType != null)
            {
                cars = cars
                    .Where(c => c.CoupeType == coupeType)
                    .ToList();
            }
            if (doorConfig != null)
            {
                cars = cars
                    .Where(c => c.DoorConfig == doorConfig)
                    .ToList();
            }

            decimal minPrice = 0;
            decimal maxPrice = 0;
            if (priceMin != null && decimal.TryParse(priceMin, out minPrice))
            {
                cars = cars
                    .Where(c => decimal.Parse(c.Price) >= minPrice)
                    .ToList();
            }
            if(priceMax != null && decimal.TryParse(priceMax, out maxPrice))
            {
                cars = cars
                    .Where(c => decimal.Parse(c.Price) <= maxPrice)
                    .ToList();
            }

            return View(cars);
        }

        public async Task<IActionResult> Details(string id)
        {
            var car = await service.GetCarDetails(id);

            return View(car);
        }

        public async Task<IActionResult> ByBrand(string id)
        {
            var model = await service.GetCarsByBrand(int.Parse(id));

            return View(model);
        }
    }
}
