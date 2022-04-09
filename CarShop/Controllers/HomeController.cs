using CarShop.Core.Constants;
using CarShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole(UserConstants.Roles.Administrator))
            {
                return RedirectToAction(nameof(Areas.Admin.Controllers.HomeController.Index),
                            nameof(Areas.Admin.Controllers.HomeController).Replace("Controller", ""),
                            new { area = nameof(Areas.Admin) });
            }
            else if (User.IsInRole(UserConstants.Roles.Employee))
            {
                return RedirectToAction(nameof(Areas.Emp.Controllers.HomeController.Index),
                            nameof(Areas.Emp.Controllers.HomeController).Replace("Controller", ""),
                            new { area = nameof(Areas.Emp) });
            }

            return View();
        }

        public async Task<IActionResult> AboutUs()
        {
            return View();
        }

        public async Task<IActionResult> Contacts()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}