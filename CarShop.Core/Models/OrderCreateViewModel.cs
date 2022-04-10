using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Core.Models
{
    public class OrderCreateViewModel
    {
        public string CarId { get; set; }

        public string CarImageUrl { get; set; }

        public string Car { get; set; }

        public string Price { get; set; }

        public string TransmissionTypeId { get; set; }

        public string ColorId { get; set; }

        public string[] ExtraIds { get; set; }

    }
}
