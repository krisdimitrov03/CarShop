using CarShop.Core.Contracts;
using CarShop.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarShop.Areas.Emp.Controllers
{
    public class CarsController : BaseController
    {
        private readonly ICarService service;
        private readonly IBrandService brandService;

        public CarsController(
            ICarService _service,
            IBrandService _brandService)
        {
            service = _service;
            brandService = _brandService;
        }

        public async Task<IActionResult> Create()
        {
            var data = await service.GetCreateDisplayInfo();

            ViewBag.Brands = data.Brands
                .ToList()
                .Select(n => new SelectListItem()
                {
                    Text = n.Name,
                    Value = n.Id.ToString(),
                    Selected = false
                }).ToList();

            ViewBag.CoupeTypes = data.CoupeTypes
                .ToList()
                .Select(n => new SelectListItem()
                {
                    Text = n.Name,
                    Value = n.Id.ToString(),
                    Selected = false
                }).ToList();

            ViewBag.DoorConfigs = data.DoorConfigs
                .ToList()
                .Select(n => new SelectListItem()
                {
                    Text = n.Name,
                    Value = n.Id.ToString(),
                    Selected = false
                }).ToList();

            ViewBag.Engines = data.Engines
                .ToList()
                .Select(n => new SelectListItem()
                {
                    Text = $"{n.EngineType.Name} - {n.HorsePower}hp",
                    Value = n.Id.ToString(),
                    Selected = false
                }).ToList();

            ViewBag.DriveTrainTypes = data.DriveTrainTypes
                .ToList()
                .Select(n => new SelectListItem()
                {
                    Text = n.Name,
                    Value = n.Id.ToString(),
                    Selected = false
                }).ToList();

            return View(new CarCreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarCreateViewModel model)
        {
            return Ok(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var model = await service.GetCarForEdit(id);

            var data = await service.GetCreateDisplayInfo();

            ViewBag.Brands = data.Brands
                .ToList()
                .Select(n => new SelectListItem()
                {
                    Text = n.Name,
                    Value = n.Id.ToString(),
                    Selected = n.Id.ToString() == model.BrandName
                }).ToList();

            ViewBag.CoupeTypes = data.CoupeTypes
                .ToList()
                .Select(n => new SelectListItem()
                {
                    Text = n.Name,
                    Value = n.Id.ToString(),
                    Selected = n.Id.ToString() == model.CoupeTypeName
                }).ToList();

            ViewBag.DoorConfigs = data.DoorConfigs
                .ToList()
                .Select(n => new SelectListItem()
                {
                    Text = n.Name,
                    Value = n.Id.ToString(),
                    Selected = n.Id.ToString() == model.DoorConfigName
                }).ToList();

            ViewBag.Engines = data.Engines
                .ToList()
                .Select(n => new SelectListItem()
                {
                    Text = $"{n.EngineType.Name} - {n.HorsePower}hp",
                    Value = n.Id.ToString(),
                    Selected = n.Id.ToString() == model.EngineName
                }).ToList();

            ViewBag.DriveTrainTypes = data.DriveTrainTypes
                .ToList()
                .Select(n => new SelectListItem()
                {
                    Text = n.Name,
                    Value = n.Id.ToString(),
                    Selected = n.Id.ToString() == model.DriveTrainTypeName
                }).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CarCreateViewModel model)
        {
            return Ok(model);
        }
    }
}
