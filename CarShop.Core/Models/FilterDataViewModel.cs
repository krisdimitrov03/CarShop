using CarShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Core.Models
{
    public class FilterDataViewModel
    {
        public FuelType[] FuelTypes { get; set; }

        public CoupeType[] CoupeTypes { get; set; }

        public DoorConfig[] DoorConfigs { get; set; }
    }
}
