﻿using CarShop.Core.Contracts;
using CarShop.Core.Models;
using CarShop.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly UserManager<ApplicationUser> userManager;

        private readonly IUserService service;

        public UserController(
            RoleManager<IdentityRole> _roleManager,
            UserManager<ApplicationUser> _userManager,
            IUserService _service)
        {
            roleManager = _roleManager;
            userManager = _userManager;
            service = _service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ManageUsers()
        {
            var users = await service.GetUsers();

            return View(users);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var model = await service.GetUserForEdit(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await service.UpdateUser(model))
            {
                return RedirectToAction(nameof(ManageUsers));
            }

            return View(model);
        }

        public async Task<IActionResult> Roles(string id)
        {
            var user = await service.GetUserById(id);
            var model = new UserRolesViewModel()
            {
                UserId = user.Id,
                Name = $"{user.FirstName} {user.LastName}"
            };


            ViewBag.RoleItems = roleManager.Roles
                .ToList()
                .Select(r => new SelectListItem()
                {
                    Text = r.Name,
                    Value = r.Name,
                    Selected = userManager.IsInRoleAsync(user, r.Name).Result
                }).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Roles(UserRolesViewModel returnedModel)
        {
            var user = await service.GetUserById(returnedModel.UserId);
            var userRoles = await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, userRoles);

            if (returnedModel.RoleNames?.Length > 0)
            {
                await userManager.AddToRolesAsync(user, returnedModel.RoleNames);
            }

            return RedirectToAction(nameof(ManageUsers));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await service.GetUserById(id);

            var model = new UserDeleteViewModel()
            {
                Id = user.Id,
                Name = $"{user.FirstName} {user.LastName}"
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UserDeleteViewModel model)
        {
            if (await service.DeleteUser(model.Id))
            {
                return RedirectToAction(nameof(ManageUsers));
            }

            else return View(model);
        }

        public async Task<ActionResult> CreateRole()
        {
            //await roleManager.CreateAsync(new IdentityRole()
            //{
            //    Name = "Employee"
            //});

            return Ok();
        }
    }
}