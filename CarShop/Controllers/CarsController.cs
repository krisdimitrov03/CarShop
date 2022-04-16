using CarShop.Core.Contracts;
using CarShop.Core.Models;
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
            string? sortBy = null,
            string? fuelType = null,
            string? coupeType = null,
            string? doorConfig = null
            )
        {
            var cars = await service.All();

            cars = await ApplyFilters(cars, sortBy, fuelType, coupeType, doorConfig);

            return View(cars);
        }


        public async Task<IActionResult> Details(string id)
        {
            var car = await service.GetCarDetails(id);

            return View(car);
        }

        public async Task<IActionResult> ByBrand(
            string id,
            string? sortBy = null,
            string? fuelType = null,
            string? coupeType = null,
            string? doorConfig = null)
        {
            var model = await service.GetCarsByBrand(int.Parse(id));
            model.Cars = await ApplyFilters(model.Cars, sortBy, fuelType, coupeType, doorConfig);

            return View(model);
        }

        private async Task<IEnumerable<CarFilterViewModel>> ApplyFilters(IEnumerable<CarFilterViewModel> cars,
            string? sortBy = null,
            string? fuelType = null,
            string? coupeType = null,
            string? doorConfig = null)
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

            if (fuelType != null)
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

            if (sortBy != null)
            {
                if (sortBy == "Price")
                {
                    cars = cars.OrderBy(c => c.Price);
                }
                if (sortBy == "PriceDesc")
                {
                    cars = cars.OrderByDescending(c => c.Price);
                }
                if (sortBy == "HorsePower")
                {
                    cars = cars.OrderBy(c => c.HorsePower);
                }
                if (sortBy == "HorsePowerDesc")
                {
                    cars = cars.OrderByDescending(c => c.HorsePower);
                }
                if (sortBy == "ReleaseYear")
                {
                    cars = cars.OrderBy(c => c.ReleaseYear);
                }
                if (sortBy == "ReleaseYearDesc")
                {
                    cars = cars.OrderByDescending(c => c.ReleaseYear);
                }
            }

            return cars;
        }
    }
}
