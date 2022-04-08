using CarShop.Core.Contracts;
using CarShop.Core.Models;
using CarShop.Infrastructure.Data;
using CarShop.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8602 // Dereference of a possibly null reference.

namespace CarShop.Core.Services
{
    public class CarService : ICarService
    {
        private readonly IApplicationDbRepository repo;

        public CarService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task<IEnumerable<CarCardViewModel>> GetAll()
        {
            return await repo.All<Car>()
                .Select(c => new CarCardViewModel()
                {
                    Id = c.Id.ToString(),
                    Brand = c.Brand.Name,
                    Model = c.Model,
                    Price = c.Price.ToString(),
                    ImageUrl = c.Images
                    .FirstOrDefault(i => i.IsProfile == true).ImageUrl
                })
                .ToListAsync();
        }

        public async Task<CarDetailsViewModel> GetCarDetails(string carId)
        {
            var car = await repo.All<Car>()
                .Include(c => c.Images)
                .Where(c => c.Id == Guid.Parse(carId))
                .FirstOrDefaultAsync();

            return new CarDetailsViewModel()
            {
                Id = car.Id.ToString(),
                ImageUrl = car.Images.FirstOrDefault(i => i.IsProfile).ImageUrl,
                Model = car.Model,
                Price = car.Price,
                Height = car.Height,
                Width = car.Width,
                Length = car.Length,
                Weight = car.Weight,
                ReleaseYear = car.ReleaseYear
            };
        }
    }
}
