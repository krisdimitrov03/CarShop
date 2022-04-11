using CarShop.Core.Constants;
using CarShop.Core.Contracts;
using CarShop.Core.Models;
using CarShop.Infrastructure.Data.Identity;
using CarShop.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarShop.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbRepository repo;
        private readonly UserManager<ApplicationUser> manager;

        public UserService(
            IApplicationDbRepository _repo,
            UserManager<ApplicationUser> _manager)
        {
            manager = _manager;
            repo = _repo;
        }

        public async Task<bool> DeleteUser(string id)
        {
            bool result = false;

            var user = await repo.GetByIdAsync<ApplicationUser>(id);

            if(user != null)
            {
                await repo.DeleteAsync<ApplicationUser>(id);

                await repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }

        public async Task<IEnumerable<UserListViewModel>> GetClients()
        {
            var clients = new List<ApplicationUser>();

            var users = await repo.All<ApplicationUser>().ToListAsync();

            foreach (var user in users)
            {
                if(!await manager.IsInRoleAsync(user, UserConstants.Roles.Administrator) &&
                   !await manager.IsInRoleAsync(user, UserConstants.Roles.Employee))
                {
                    clients.Add(user);
                }
            }

            return clients.Select(u => new UserListViewModel()
            {
                Name = $"{u.FirstName} {u.LastName}",
                Email = u.Email
            }).ToList();
        }

        public async Task<IEnumerable<UserListViewModel>> GetEmployees()
        {
            var users = await manager.GetUsersInRoleAsync(UserConstants.Roles.Employee);

            return users.Select(u => new UserListViewModel()
            {
                Name = $"{u.FirstName} {u.LastName}",
                Email = u.Email
            }).ToList();
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            return await repo.GetByIdAsync<ApplicationUser>(id);
        }

        public async Task<UserEditViewModel> GetUserForEdit(string id)
        {
            var user = await repo.GetByIdAsync< ApplicationUser>(id);

            return new UserEditViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public async Task<IEnumerable<UserListViewModel>> GetUsers()
        {
            return await repo.All<ApplicationUser>()
                .Select(u => new UserListViewModel()
                {
                    Id = u.Id,
                    Name = $"{u.FirstName} {u.LastName}",
                    Email = u.Email
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateUser(UserEditViewModel model)
        {
            bool result = false;
            var user = await repo.GetByIdAsync<ApplicationUser>(model.Id);

            if(user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;

                await repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }
    }
}
