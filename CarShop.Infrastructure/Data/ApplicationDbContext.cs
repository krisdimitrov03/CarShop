using CarShop.Infrastructure.Data.Identity;
using CarShop.Infrastructure.Seeders;
using CarShop.Infrastructure.Seeders.Contracts;
using Microsoft.AspNetCore.Identity;
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
        public DbSet<DriveTrainType> DriveTrainTypes { get; set; }
        public DbSet<TransmissionType> TransmissionTypes { get; set; }
        public DbSet<DoorConfig> DoorConfigs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Extra> Extras { get; set; }
        public DbSet<OrdersExtra> OrdersExtras { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
            base.OnModelCreating(builder);

			ISeeder<Color> colorSeeder = new Seeder<Color>();
			ISeeder<Brand> brandSeeder = new Seeder<Brand>();
			ISeeder<CoupeType> coupeTypeSeeder = new Seeder<CoupeType>();
			ISeeder<DoorConfig> doorConfigSeeder = new Seeder<DoorConfig>();
			ISeeder<DriveTrainType> driveTrainTypeSeeder = new Seeder<DriveTrainType>();
			ISeeder<Engine> engineSeeder = new Seeder<Engine>();
			ISeeder<EngineType> engineTypeSeeder = new Seeder<EngineType>();
			ISeeder<FuelType> fuelTypeSeeder = new Seeder<FuelType>();
			ISeeder<TransmissionType> transmissionTypeSeeder = new Seeder<TransmissionType>();
			ISeeder<Extra> extraSeeder = new Seeder<Extra>();

			builder.Entity<Color>().HasData(colorSeeder.GetData());
			builder.Entity<Brand>().HasData(brandSeeder.GetData());
			builder.Entity<CoupeType>().HasData(coupeTypeSeeder.GetData());
			builder.Entity<DoorConfig>().HasData(doorConfigSeeder.GetData());
			builder.Entity<DriveTrainType>().HasData(driveTrainTypeSeeder.GetData());
			builder.Entity<Engine>().HasData(engineSeeder.GetData());
			builder.Entity<EngineType>().HasData(engineTypeSeeder.GetData());
			builder.Entity<FuelType>().HasData(fuelTypeSeeder.GetData());
			builder.Entity<TransmissionType>().HasData(transmissionTypeSeeder.GetData());
			builder.Entity<Extra>().HasData(extraSeeder.GetData());
		}
	}
}