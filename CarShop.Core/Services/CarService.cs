using CarShop.Core.Contracts;
using CarShop.Core.Models;
using CarShop.Infrastructure.Data;
using CarShop.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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
                .Include(c => c.Brand)
                .Select(c => new CarFilterViewModel()
                {
                    Id = c.Id.ToString(),
                    Brand = c.BrandId.ToString(),
                    BrandName = c.Brand.Name,
                    ImageUrl = c.Images.FirstOrDefault(i => i.IsProfile).ImageUrl,
                    Model = c.Model,
                    Price = c.Price.ToString()
                })
                .ToListAsync();

            return new CarsByBrandViewModel()
            {
                BrandId = brandId.ToString(),
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

        public async Task<IEnumerable<CarFilterViewModel>> All()
        {
            return await repo.All<Car>()
                .Include(c => c.Images)
                .Include(c => c.Brand)
                .Include(c => c.Engine)
                .ThenInclude(e => e.EngineType)
                .Select(c => new CarFilterViewModel()
                {
                    Id= c.Id.ToString(),
                    Brand = c.BrandId.ToString(),
                    BrandName = c.Brand.Name,
                    ImageUrl = c.Images.FirstOrDefault(i => i.IsProfile).ImageUrl,
                    Model= c.Model,
                    ReleaseYear = c.ReleaseYear.ToString(),
                    HorsePower = c.Engine.HorsePower.ToString(),
                    FuelType = c.Engine.EngineType.FuelTypeId.ToString(),
                    CoupeType = c.CoupeTypeId.ToString(),
                    DoorConfig = c.DoorConfigId.ToString(),
                    Price= c.Price.ToString()
                })
                .ToListAsync();
        }

        public async Task<CarDetailsViewModel> GetCarDetails(string carId)
        {
            var car = await repo.All<Car>()
                .Include(c => c.Images)
                .Include(c => c.Engine)
                .ThenInclude(c => c.EngineType)
                .Include(c => c.DoorConfig)
                .Include(c => c.CoupeType)
                .Include(c => c.Brand)
                .Where(c => c.Id == Guid.Parse(carId))
                .FirstOrDefaultAsync();

            return new CarDetailsViewModel()
            {
                Id = car.Id.ToString(),
                ImageUrl = car.Images.FirstOrDefault(i => i.IsProfile).ImageUrl,
                OtherImagesUrls = car.Images
                .Where(i => !i.IsProfile)
                .Select(i => i.ImageUrl)
                .ToArray(),
                Brand = car.Brand.Name,
                Model = car.Model,
                Engine = car.Engine.EngineType.Name,
                DoorConfig = car.DoorConfig.Name,
                CoupeType = car.CoupeType.Name,
                HorsePower = car.Engine.HorsePower.ToString(),
                FuelConsumption = car.FuelConsumption.ToString(),
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
                    ImageUrls = string.Join(" || ", c.Images.Where(i => !i.IsProfile).Select(i => i.ImageUrl).ToArray())
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
                car.Price = decimal.Parse(returnedModel.Price, CultureInfo.InvariantCulture);

                repo.DeleteRange(car.Images);

                await repo.SaveChangesAsync();

                var profileImage = new Image()
                {
                    ImageUrl = returnedModel.ProfileImageUrl,
                    IsProfile = true
                };

                await repo.AddAsync(profileImage);

                if(returnedModel.Urls.Length > 0)
                {
                    var otherImages = returnedModel.Urls
                        .Select(n => new Image()
                        {
                            ImageUrl = n,
                            IsProfile = false
                        }).ToList();

                    await repo.AddRangeAsync(otherImages);
                    await repo.SaveChangesAsync();

                    car.Images = new List<Image>(otherImages);
                }

                car.Images.Add(profileImage);

                await repo.SaveChangesAsync();

                return (true,car.Id.ToString());
            }

            return (false, car.Id.ToString());
        }

        public async Task<FilterDataViewModel> GetFilterData()
        {
            var fuelTypes = await repo.All<FuelType>()
                .ToArrayAsync();

            var coupeTypes = await repo.All<CoupeType>()
                .ToArrayAsync();

            var doorConfigs = await repo.All<DoorConfig>()
                .ToArrayAsync();

            return new FilterDataViewModel()
            {
                FuelTypes = fuelTypes,
                DoorConfigs = doorConfigs,
                CoupeTypes = coupeTypes
            };
        }

        public async Task<(CarCardViewModel, int)> GetMostSaledCar()
        {
            var cars = await repo.All<Car>()
                .Include(c => c.Brand)
                .Include(c => c.Images)
                .ToListAsync();

            int count = 0;
            Car? mostSaledCar = null;

            foreach (var car in cars)
            {
                var orders = await repo.All<Order>()
                    .Where(o => o.CarId == car.Id)
                    .ToListAsync();

                if(orders.Count > count)
                {
                    count = orders.Count;
                    mostSaledCar = car;
                }
            }

            return (new CarCardViewModel()
            {
                Id = mostSaledCar == null ? "" : mostSaledCar.Id.ToString(),
                Brand = mostSaledCar == null ? "" : mostSaledCar.Brand.Name,
                Model = mostSaledCar == null ? "" : mostSaledCar.Model,
                ImageUrl = mostSaledCar == null ? "" : mostSaledCar.Images
                .FirstOrDefault(i => i.IsProfile).ImageUrl,
                Price = mostSaledCar == null ? "" : mostSaledCar.Price.ToString()
            }, count);
        }
    }
}
