using CarShop.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarShop.Areas.Emp.Controllers
{
    [Authorize(Roles = $"{UserConstants.Roles.Employee}, {UserConstants.Roles.Administrator}")]
    [Area("Emp")]
    public class BaseController : Controller
    {
    }
}
