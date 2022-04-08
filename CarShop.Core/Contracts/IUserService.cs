using CarShop.Core.Models;
using CarShop.Infrastructure.Data.Identity;

namespace CarShop.Core.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserListViewModel>> GetUsers();

        Task<UserEditViewModel> GetUserForEdit(string id);

        Task<bool> UpdateUser(UserEditViewModel model);

        Task<ApplicationUser> GetUserById(string id);

        Task<bool> DeleteUser(string id);
    }
}
