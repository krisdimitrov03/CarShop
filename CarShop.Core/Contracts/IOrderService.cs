using CarShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Core.Contracts
{
    public interface IOrderService
    {
        Task<ExtrasDbInfoViewModel> GetInfoForNewOrder();
        Task<bool> Create(string carId, string[] extraIds, string colorId, string transmissionTypeId, ClaimsPrincipal user);
        Task<IEnumerable<OrderListViewModel>> GetPersonal(string id);
        Task<IEnumerable<OrderListViewModel>> GetAll();
    }
}
