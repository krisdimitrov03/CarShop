using CarShop.Core.Contracts;
using CarShop.Core.Models;
using CarShop.Infrastructure.Data;
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
        public async Task<IActionResult> Create(CarCreateViewModel returnedModel)
        {
            var profileImage = new Image()
            {
                ImageUrl = returnedModel.ProfileImageUrl,
                IsProfile = true
            };

            await service.CreateImagesForCar(profileImage);

            Car car = new Car()
            {
                BrandId = int.Parse(returnedModel.BrandId),
                CoupeTypeId = int.Parse(returnedModel.CoupeTypeId),
                CrashProtectionLevel = double.Parse(returnedModel.CrashProtectionLevel),
                DoorConfigId = int.Parse(returnedModel.DoorConfigId),
                DriveTrainTypeId = int.Parse(returnedModel.DriveTrainTypeId),
                EngineId = int.Parse(returnedModel.EngineId),
                FuelConsumption = double.Parse(returnedModel.FuelConsumption),
                Height = int.Parse(returnedModel.Heigth),
                Width = int.Parse(returnedModel.Width),
                Length = int.Parse(returnedModel.Length),
                Weight = int.Parse(returnedModel.Weight),
                Model = returnedModel.Model,
                ReleaseYear = int.Parse(returnedModel.ReleaseYear),
                Price = decimal.Parse(returnedModel.Price),
                Images = new List<Image>()
            };

            car.Images.Add(profileImage);

            if(await service.CreateCar(car))
            {
                return RedirectToAction(nameof(CarShop.Controllers.CarsController.All),
                    nameof(CarShop.Controllers.CarsController).Replace("Controller", ""),
                    new { area="" });
            }

            return View();
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
                    Selected = n.Id.ToString() == model.BrandId
                }).ToList();

            ViewBag.CoupeTypes = data.CoupeTypes
                .ToList()
                .Select(n => new SelectListItem()
                {
                    Text = n.Name,
                    Value = n.Id.ToString(),
                    Selected = n.Id.ToString() == model.CoupeTypeId
                }).ToList();

            ViewBag.DoorConfigs = data.DoorConfigs
                .ToList()
                .Select(n => new SelectListItem()
                {
                    Text = n.Name,
                    Value = n.Id.ToString(),
                    Selected = n.Id.ToString() == model.DoorConfigId
                }).ToList();

            ViewBag.Engines = data.Engines
                .ToList()
                .Select(n => new SelectListItem()
                {
                    Text = $"{n.EngineType.Name} - {n.HorsePower}hp",
                    Value = n.Id.ToString(),
                    Selected = n.Id.ToString() == model.EngineId
                }).ToList();

            ViewBag.DriveTrainTypes = data.DriveTrainTypes
                .ToList()
                .Select(n => new SelectListItem()
                {
                    Text = n.Name,
                    Value = n.Id.ToString(),
                    Selected = n.Id.ToString() == model.DriveTrainTypeId
                }).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CarCreateViewModel returnedModel)
        {
            var (result, id) = await service.Update(returnedModel);
            return RedirectToAction(nameof(CarShop.Controllers.CarsController.Details),
                            nameof(CarShop.Controllers.CarsController).Replace("Controller", ""),
                            new { area = "", id = id });
        }

        public async Task<IActionResult> Delete(string id)
        {
            var car = await service.GetById(id);

            return View(car);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CarCardViewModel returnedModel)
        {
            var id = Guid.Parse(returnedModel.Id);

            if (await service.RemoveCar(id))
            {
                return RedirectToAction(nameof(CarShop.Controllers.CarsController.All),
                    nameof(CarShop.Controllers.CarsController).Replace("Controller", ""),
                    new { area = "" });
            }
            else return RedirectToAction(nameof(CarShop.Controllers.CarsController.Details),
                    nameof(CarShop.Controllers.CarsController).Replace("Controller", ""),
                    new { area = "", id = returnedModel.Id });
        }
    }
}
