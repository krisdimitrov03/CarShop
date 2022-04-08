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
        Task<IEnumerable<CarCardViewModel>> GetAll();
        Task<CarDetailsViewModel> GetCarDetails(string carId);
    }
}
