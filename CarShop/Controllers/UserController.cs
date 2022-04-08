using CarShop.Core.Constants;
using CarShop.Core.Contracts;
using CarShop.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarShop.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        private readonly IUserService service;

        public UserController(
            UserManager<ApplicationUser> _userManager,
            IUserService _service)
        {
            userManager = _userManager;
            service = _service;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
