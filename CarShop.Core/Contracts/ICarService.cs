using CarShop.Core.Models;
using CarShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Core.Contracts
{
    public interface ICarService
    {
        Task<FilterDataViewModel> GetFilterData();
        Task<IEnumerable<CarFilterViewModel>> All();
        Task<IEnumerable<CarCardViewModel>> GetAll();
        Task<CarDetailsViewModel> GetCarDetails(string carId);
        Task<DbInfoViewModel> GetCreateDisplayInfo();
        Task<CarCreateViewModel> GetCarForEdit(string carId);
        Task<CarsByBrandViewModel> GetCarsByBrand(int brandId);
        Task<CarCardViewModel> GetById(string carId);
        Task<bool> CreateCar(Car car);
        Task<bool> RemoveCar(Guid? carId);
        Task<bool> CreateImagesForCar(List<Image> images);
        Task<bool> CreateImagesForCar(Image image);
        Task<(bool, string)> Update(CarCreateViewModel returnedModel);
        Task<(CarCardViewModel, int)> GetMostSaledCar();
    }
}
