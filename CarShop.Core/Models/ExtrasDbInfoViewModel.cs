using CarShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Core.Models
{
    public class ExtrasDbInfoViewModel
    {
        public Color[] Colors { get; set; }

        public TransmissionType[] TransmissionTypes { get; set; }

        public Extra[] Extras { get; set; }
    }
}
