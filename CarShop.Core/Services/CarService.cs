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

        public async Task<CarsByBrandViewModel> GetCarsByBrand(int brandId)
        {
            var brand = await repo.GetByIdAsync<Brand>(brandId);

            var cars = await repo.All<Car>()
                .Where(c => c.BrandId == brandId)
                .Include(c => c.Images)
                .Select(c => new CarCardViewModel()
                {
                    Id = c.Id.ToString(),
                    Brand = c.Brand.Name,
                    ImageUrl = c.Images.FirstOrDefault(i => i.IsProfile).ImageUrl,
                    Model = c.Model,
                    Price = c.Price.ToString()
                })
                .ToListAsync();

            return new CarsByBrandViewModel()
            {
                BrandName = brand.Name,
                Cars = cars
            };
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

        public async Task<CarCreateViewModel> GetCarForEdit(string carId)
        {
            return await repo.All<Car>()
                .Where(c => c.Id.ToString() == carId)
                .Include(c => c.Images)
                .Select(c => new CarCreateViewModel()
                {
                    BrandName = c.BrandId.ToString(),
                    Model = c.Model,
                    ReleaseYear = c.ReleaseYear.ToString(),
                    Heigth = c.Height.ToString(),
                    Width = c.Width.ToString(),
                    Length= c.Length.ToString(),
                    Weight= c.Weight.ToString(),
                    CoupeTypeName = c.CoupeTypeId.ToString(),
                    DoorConfigName = c.DoorConfigId.ToString(),
                    CrashProtectionLevel = c.CrashProtectionLevel.ToString(),
                    FuelConsumption = c.FuelConsumption.ToString(),
                    EngineName = c.EngineId.ToString(),
                    DriveTrainTypeName = c.DriveTrainTypeId.ToString(),
                    Price = c.Price.ToString(),
                    ImageUrls = c.Images.Select(i => i.ImageUrl).ToArray()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<DbInfoViewModel> GetCreateDisplayInfo()
		{
			var brands = await repo.All<Brand>()
                .ToArrayAsync();

            var coupeTypes = await repo.All<CoupeType>()
                .ToArrayAsync();

            var doorConfigs = await repo.All<DoorConfig>()
                .ToArrayAsync();

            var engines = await repo.All<Engine>()
                .Include(e => e.EngineType)
                .ToArrayAsync();

            var driveTrainTypes = await repo.All<DriveTrainType>()
                .ToArrayAsync();

            return new DbInfoViewModel()
            {
                Brands = brands,
                CoupeTypes = coupeTypes,
                DoorConfigs = doorConfigs,
                Engines = engines,
                DriveTrainTypes = driveTrainTypes
            };
        }

        public async Task<CarCardViewModel> GetById(string carId)
        {
            return await repo.All<Car>()
                .Where(c => c.Id == Guid.Parse(carId))
                .Include(c => c.Brand)
                .Include(c => c.Images)
                .Select(c => new CarCardViewModel()
                {
                    Id = c.Id.ToString(),
                    Brand = c.Brand.Name,
                    Model = c.Model,
                    Price = c.Price.ToString(),
                    ImageUrl = c.Images
                    .FirstOrDefault(i => i.IsProfile == true).ImageUrl
                })
                .FirstOrDefaultAsync();
        }
    }
}
