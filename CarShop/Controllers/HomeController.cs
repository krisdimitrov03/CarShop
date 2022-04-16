using CarShop.Core.Constants;
using CarShop.Core.Contracts;
using CarShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace CarShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService userService;

        public HomeController(
            ILogger<HomeController> logger,
            IUserService _service)
        {
            _logger = logger;
            userService = _service;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var firstName = userService.GetUserById(id).Result.FirstName;
                ViewBag.FirstName = firstName;
            }

            if (User.IsInRole(UserConstants.Roles.Administrator))
            {
                return RedirectToAction(nameof(Areas.Admin.Controllers.HomeController.Index),
                            nameof(Areas.Admin.Controllers.HomeController).Replace("Controller", ""),
                            new { area = nameof(Areas.Admin) });
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