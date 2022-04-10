using CarShop.Core.Contracts;
using CarShop.Core.Models;
using CarShop.Infrastructure.Data;
using CarShop.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
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
                    Id = c.Id.ToString(),
                    BrandId = c.BrandId.ToString(),
                    Model = c.Model,
                    ReleaseYear = c.ReleaseYear.ToString(),
                    Heigth = c.Height.ToString(),
                    Width = c.Width.ToString(),
                    Length = c.Length.ToString(),
                    Weight = c.Weight.ToString(),
                    CoupeTypeId = c.CoupeTypeId.ToString(),
                    DoorConfigId = c.DoorConfigId.ToString(),
                    CrashProtectionLevel = c.CrashProtectionLevel.ToString(),
                    FuelConsumption = c.FuelConsumption.ToString(),
                    EngineId = c.EngineId.ToString(),
                    DriveTrainTypeId = c.DriveTrainTypeId.ToString(),
                    Price = c.Price.ToString(),
                    ProfileImageUrl = c.Images.FirstOrDefault(i => i.IsProfile).ImageUrl,
                    ImageUrls = c.Images.Where(i => !i.IsProfile).Select(i => i.ImageUrl).ToArray()
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

        public async Task<bool> RemoveCar(Guid? carId)
        {
            if (carId == null)
                return false;

            var car = await repo.All<Car>()
                .Where(c => c.Id == carId)
                .Include(c => c.Images)
                .FirstOrDefaultAsync();

            if (car != null)
            {
                var orders = await repo.All<Order>()
                    .Where(o => o.CarId == carId)
                    .ToListAsync();

                var ordersExtras = new List<OrdersExtra>();

                foreach (var order in orders)
                {
                    var currentOrdersExtras = await repo.All<OrdersExtra>()
                        .Where(oe => oe.Order == order).ToListAsync();

                    ordersExtras.AddRange(currentOrdersExtras);
                }

                var images = car.Images;

                if (ordersExtras.Count > 0)
                {
                    repo.DeleteRange(ordersExtras);
                    await repo.SaveChangesAsync();
                }

                if (images.Count > 0)
                {
                    repo.DeleteRange(images);
                    await repo.SaveChangesAsync();
                }

                if (orders.Count > 0)
                {
                    repo.DeleteRange(orders);
                    await repo.SaveChangesAsync();
                }

                await repo.DeleteAsync<Car>(car.Id);
                await repo.SaveChangesAsync();

                return true;
            }
            else return false;
        }

        public async Task<bool> CreateCar(Car car)
        {
            if (car == null)
            {
                return false;
            }

            await repo.AddAsync(car);
            await repo.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CreateImagesForCar(List<Image> images)
        {
            if (images == null || images.Count == 0)
            {
                return false;
            }

            await repo.AddRangeAsync(images);
            await repo.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CreateImagesForCar(Image image)
        {
            if (image == null)
            {
                return false;
            }

            await repo.AddAsync(image);
            await repo.SaveChangesAsync();

            return true;
        }

        public async Task<(bool, string)> Update(CarCreateViewModel returnedModel)
        {
            var car = await repo.All<Car>()
                .Where(c => c.Id.ToString() == returnedModel.Id)
                .Include(c => c.Images)
                .FirstOrDefaultAsync();

            if(car != null)
            {
                car.BrandId = int.Parse(returnedModel.BrandId);
                car.Model = returnedModel.Model;
                car.ReleaseYear = int.Parse(returnedModel.ReleaseYear);
                car.Height = int.Parse(returnedModel.Heigth);
                car.Width = int.Parse(returnedModel.Width);
                car.Length = int.Parse(returnedModel.Length);
                car.Weight = int.Parse(returnedModel.Weight);
                car.CoupeTypeId = int.Parse(returnedModel.CoupeTypeId);
                car.DoorConfigId = int.Parse(returnedModel.DoorConfigId);
                car.CrashProtectionLevel = double.Parse(returnedModel.CrashProtectionLevel);
                car.FuelConsumption = double.Parse(returnedModel.FuelConsumption);
                car.EngineId = int.Parse(returnedModel.EngineId);
                car.DriveTrainTypeId = int.Parse(returnedModel.DriveTrainTypeId);
                car.Price = decimal.Parse(returnedModel.Price);
                car.Images
                    .FirstOrDefault(i => i.IsProfile).ImageUrl = returnedModel.ProfileImageUrl;

                await repo.SaveChangesAsync();
                return (true,car.Id.ToString());
            }

            return (false, car.Id.ToString());
        }
    }
}
