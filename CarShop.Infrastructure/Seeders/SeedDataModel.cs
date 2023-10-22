
using CarShop.Infrastructure.Data;
using CarShop.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;

namespace CarShop.Infrastructure.Seeders
{
    public class SeedDataModel
    {
        public ApplicationUser[] Users { get; set; }
        public IdentityRole[] Roles { get; set; }
        public IdentityUserRole<string>[] UsersRoles { get; set; }
        public Color[] Colors { get; set; }
        public Brand[] Brands { get; set; }
        public CoupeType[] CoupeTypes { get; set; }
        public DoorConfig[] DoorConfigs { get; set; }
        public DriveTrainType[] DriveTrainTypes { get; set; }
        public Engine[] Engines { get; set; }
        public EngineType[] EngineTypes { get; set; }
        public FuelType[] FuelTypes { get; set; }
        public TransmissionType[] TransmissionTypes { get; set; }
        public Extra[] Extras { get; set; }
    }
}
