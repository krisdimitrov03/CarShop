using CarShop.Core.Contracts;
using CarShop.Core.Models;
using CarShop.Infrastructure.Data;
using CarShop.Infrastructure.Data.Identity;
using CarShop.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IApplicationDbRepository repo;

        public OrderService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task<bool> Create(string carId, string[] extraIds, string colorId, string transmissionTypeId, ClaimsPrincipal _user)
        {
            if (carId == null ||
                extraIds.Length < 0 ||
                extraIds == null ||
                transmissionTypeId == null ||
                colorId == null)
            {
                return false;
            }

            var userId = _user.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await repo.GetByIdAsync<ApplicationUser>(userId);
            var car = await repo.GetByIdAsync<Car>(Guid.Parse(carId));

            var transmissionType = await repo.GetByIdAsync<TransmissionType>(int.Parse(transmissionTypeId));

            var color = await repo.GetByIdAsync<Color>(int.Parse(colorId));

            var extras = await repo.All<Extra>()
                .Where(e => extraIds.Contains(e.Id.ToString()))
                .ToListAsync();

            if (user == null || 
                car == null || 
                transmissionType == null || 
                color == null || 
                extras.Count < 0)
            {
                return false;
            }

            Order order = new Order()
            {
                User = user,
                Car = car,
                OrderDate = DateTime.Now,
                Color = color,
                TransmissionType = transmissionType,
                TotalPrice = car.Price + extras.Select(e => e.Price).Sum()
            };

            await repo.AddAsync(order);

            await repo.SaveChangesAsync();

            foreach (var extra in extras)
            {
                await repo.AddAsync(new OrdersExtra()
                {
                    Order = order,
                    Extra = extra
                });
            }

            await repo.SaveChangesAsync();

            return true;
        }

        public async Task<ExtrasDbInfoViewModel> GetInfoForNewOrder()
        {
            var colors = await repo.All<Color>()
                .ToArrayAsync();

            var transmissionTypes = await repo.All<TransmissionType>()
                .ToArrayAsync();

            var extras = await repo.All<Extra>()
                .ToArrayAsync();

            return new ExtrasDbInfoViewModel()
            {
                Colors = colors,
                Extras = extras,
                TransmissionTypes = transmissionTypes
            };
        }

        public async Task<IEnumerable<OrderListViewModel>> GetPersonalOrders(string id)
        {
            return await repo.All<Order>()
                .Include(o => o.Car)
                .ThenInclude(c => c.Brand)
                .Include(o => o.User)
                .Include(o => o.TransmissionType)
                .Include(o => o.Color)
                .Where(o => o.UserId == id)
                .Select(o => new OrderListViewModel()
                {
                    CarBrand = o.Car.Brand.Name,
                    CarModel = o.Car.Model,
                    UserFirstName = o.User.FirstName,
                    UserLastName = o.User.LastName,
                    TransmissionType = o.TransmissionType.Name,
                    Color = o.Color.Name,
                    Date = $"{o.OrderDate.Day}" +
                           $".{o.OrderDate.Month}" +
                           $".{o.OrderDate.Year}",
                    Price = o.TotalPrice.ToString(),
                })
                .ToListAsync();
        }
    }
}
