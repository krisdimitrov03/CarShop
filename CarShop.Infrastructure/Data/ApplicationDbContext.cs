using CarShop.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarShop.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<CoupeType> CoupeTypes { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<EngineType> EngineTypes { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<SusspensionType> SusspensionTypes { get; set; }
        public DbSet<BrakesType> BrakesTypes { get; set; }
        public DbSet<WheelsType> WheelsTypes { get; set; }
        public DbSet<DriveTrainType> DriveTrainTypes { get; set; }
        public DbSet<TransmissionType> TransmissionTypes { get; set; }
        public DbSet<DoorConfig> DoorConfigs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Extra> Extras { get; set; }
    }
}