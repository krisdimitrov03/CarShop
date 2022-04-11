using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Core.Models
{
    public class CarFilterViewModel
    {
        public string Id { get; set; }

        public string ImageUrl { get; set; }

        public string Brand { get; set; }

        public string BrandName { get; set; }

        public string Model { get; set; }

        public string Price { get; set; }

        public string ReleaseYear { get; set; }

        public string HorsePower { get; set; }

        public string FuelType { get; set; }

        public string CoupeType { get; set; }

        public string DoorConfig { get; set; }
    }
}
