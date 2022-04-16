using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Core.Models
{
    public class CarDetailsViewModel
    {
        public string Id { get; set; }

        public string ImageUrl { get; set; }

        public string[] OtherImagesUrls { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

		public string Engine { get; set; }

		public string HorsePower { get; set; }

		public string CoupeType { get; set; }

		public string DoorConfig { get; set; }

		public decimal Price { get; set; }

        public int ReleaseYear { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public int Length { get; set; }

        public int Weight { get; set; }

		public string FuelConsumption { get; set; }
	}
}
