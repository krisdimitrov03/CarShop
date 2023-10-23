using CarShop.Core.Contracts;
using CarShop.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CarShop.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUserService userService;
        private readonly IOrderService orderService;
        private readonly ICarService carService;

        public HomeController(
            IUserService _userService,
            IOrderService _orderService,
            ICarService _carService)
        {
            userService = _userService;
            orderService = _orderService;
            carService = _carService;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await orderService.GetAll();
            var employees = await userService.GetEmployees();
            var clients = await userService.GetClients();
            var totalIncome = orders
                .Select(o => o.Price)
                .Select(o => decimal.Parse(o, CultureInfo.InvariantCulture))
                .Sum();

            var (mostSaledCar, count) = await carService.GetMostSaledCar();

            var model = new AdminDashboardViewModel()
            {
                Orders = orders,
                Employees = employees,
                Clients = clients,
                MostSaledCar = $"{mostSaledCar.Brand} {mostSaledCar.Model}",
                TotalIncome = totalIncome,
                CarImageUrl = mostSaledCar.ImageUrl,
                CountOfSalesOfCar = count.ToString()
            };

            return View(model);
        }
    }
}
