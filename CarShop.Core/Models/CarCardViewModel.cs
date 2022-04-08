using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Core.Models
{
    public class CarCardViewModel
    {
        public string Id { get; set; }
        public string ImageUrl { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Price { get; set; }
    }
}
