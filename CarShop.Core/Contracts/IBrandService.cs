using CarShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Core.Contracts
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandCardViewModel>> GetBrands();
    }
}
