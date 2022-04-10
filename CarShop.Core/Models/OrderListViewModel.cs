using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Core.Models
{
    public class OrderListViewModel
    {
        public string Date { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public string CarBrand { get; set; }

        public string CarModel { get; set; }

        public string Color { get; set; }

        public string TransmissionType { get; set; }

        public string Price { get; set; }
    }
}
