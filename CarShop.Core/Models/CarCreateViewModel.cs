namespace CarShop.Core.Models
{
	public class CarCreateViewModel
	{
		public string BrandName { get; set; }

		public string Model { get; set; }

		public string ReleaseYear { get; set; }

		public string Heigth { get; set; }

		public string Width { get; set; }

		public string Length { get; set; }

		public string Weight { get; set; }

		public string CoupeTypeName { get; set; }

		public string DoorConfigName { get; set; }

		public string CrashProtectionLevel { get; set; }

		public string FuelConsumption { get; set; }

		public string EngineName { get; set; }

		public string DriveTrainTypeName { get; set; }

		public string Price { get; set; }

		public string[] ImageUrls { get; set; }
	}
}
