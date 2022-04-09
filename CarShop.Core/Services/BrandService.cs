using CarShop.Core.Contracts;
using CarShop.Core.Models;
using CarShop.Infrastructure.Data;
using CarShop.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Core.Services
{
    public class BrandService : IBrandService
    {
        private readonly IApplicationDbRepository repo;

        public BrandService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task<IEnumerable<BrandCardViewModel>> GetBrands()
        {
            return await repo.All<Brand>()
                .Select(b => new BrandCardViewModel()
                {
                    Id = b.Id.ToString(),
                    Name = b.Name
                })
                .ToListAsync();
        }
    }
}
