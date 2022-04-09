using CarShop.Infrastructure.Data;

namespace CarShop.Core.Models
{
	public class DbInfoViewModel
	{
		public Brand[] Brands { get; set; }

		public CoupeType[] CoupeTypes { get; set; }

		public DoorConfig[] DoorConfigs { get; set; }

		public Engine[] Engines { get; set; }

		public DriveTrainType[] DriveTrainTypes { get; set; }
	}
}
