namespace CarShop.Core.Models
{
    public class CarsByBrandViewModel
    {
        public string BrandName { get; set; }

        public IEnumerable<CarCardViewModel> Cars { get; set; }
    }
}
