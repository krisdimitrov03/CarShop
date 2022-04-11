using CarShop.Core.Contracts;
using CarShop.Core.Models;
using CarShop.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace CarShop.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderService service;
        private readonly ICarService carService;
        private readonly IUserService userService;

        public OrdersController(
            IOrderService _service,
            ICarService _carService)
        {
            service = _service;
            carService = _carService;
        }

        public async Task<IActionResult> New(string id)
        {
            var car = await carService.GetById(id);
            var data = await service.GetInfoForNewOrder();

            ViewBag.Colors = data.Colors
                .Select(n => new SelectListItem()
                {
                    Text = n.Name,
                    Value = n.Id.ToString(),
                    Selected = false
                })
                .ToList();

            ViewBag.TransmissionTypes = data.TransmissionTypes
                .Select(n => new SelectListItem()
                {
                    Text = n.Name,
                    Value = n.Id.ToString(),
                    Selected = false
                })
                .ToList();

            ViewBag.Extras = data.Extras
                .Select(n => new SelectListItem()
                {
                    Text = n.Name,
                    Value = n.Id.ToString(),
                    Selected = false
                })
                .ToList();

            return View(new OrderCreateViewModel()
            {
                CarId = car.Id.ToString(),
                Car = $"{car.Brand} {car.Model}",
                CarImageUrl = car.ImageUrl,
                Price = car.Price
            });
        }

        [HttpPost]
        public async Task<IActionResult> New(OrderCreateViewModel model)
        {
            if (await service.Create(model.CarId, model.ExtraIds, model.ColorId, model.TransmissionTypeId, User))
            {
                return RedirectToAction(nameof(SuccessfulOrder));
            }
            else
            {
                return RedirectToAction(nameof(UnsuccessfulOrder));
            }
        }

        public async Task<IActionResult> UnsuccessfulOrder()
        {
            return View();
        }

        public async Task<IActionResult> SuccessfulOrder()
        {
            return View();
        }

        public async Task<IActionResult> Personal()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = await service.GetPersonal(id);

            return View(model);
        }
    }
}
