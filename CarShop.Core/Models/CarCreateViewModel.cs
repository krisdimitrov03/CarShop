namespace CarShop.Core.Models
{
	public class CarCreateViewModel
	{
        public string Id { get; set; }
        public string BrandId { get; set; }

		public string Model { get; set; }

		public string ReleaseYear { get; set; }

		public string Heigth { get; set; }

		public string Width { get; set; }

		public string Length { get; set; }

		public string Weight { get; set; }

		public string CoupeTypeId { get; set; }

		public string DoorConfigId { get; set; }

		public string CrashProtectionLevel { get; set; }

		public string FuelConsumption { get; set; }

		public string EngineId { get; set; }

		public string DriveTrainTypeId { get; set; }

		public string Price { get; set; }

		public string ImageUrls { get; set; }

		public string[] Urls
		{
			get
			{
				if(ImageUrls == null) return new string[0];
				else return ImageUrls.Split(" || ", StringSplitOptions.RemoveEmptyEntries);
			}
		}

        public string ProfileImageUrl { get; set; }
    }
}
