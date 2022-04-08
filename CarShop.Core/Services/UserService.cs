using CarShop.Core.Contracts;
using CarShop.Core.Models;
using CarShop.Infrastructure.Data.Identity;
using CarShop.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarShop.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbRepository repo;

        public UserService(IApplicationDbRepository _repo)
        {
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
