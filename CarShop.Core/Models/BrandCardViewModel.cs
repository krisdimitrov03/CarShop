using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Core.Models
{
    public class BrandCardViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl
        {
            get
            {
                return $"/img/logos/{Name.ToLower()}.png";
            }
        }
    }
}
