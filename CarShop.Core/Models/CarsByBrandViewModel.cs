namespace CarShop.Core.Models
{
    public class CarsByBrandViewModel
    {
        public string BrandName { get; set; }

        public IEnumerable<CarFilterViewModel> Cars { get; set; }
    }
}
