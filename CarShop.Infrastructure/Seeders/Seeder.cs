using CarShop.Infrastructure.Data;
using CarShop.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace CarShop.Infrastructure.Seeders
{
    public static class Seeder
    {
        public static IApplicationBuilder SeedData(this IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                context.Database.EnsureCreated();

                SeedDataModel data = GetData(@"../CarShop.Infrastructure/Seeders/seedData.json");

                AddData(context, data.Users);
                AddData(context, data.Roles);
                AddData(context, data.UsersRoles);
                AddData(context, data.Brands);
                AddData(context, data.Colors);
                AddData(context, data.CoupeTypes);
                AddData(context, data.DoorConfigs);
                AddData(context, data.DriveTrainTypes);
                AddData(context, data.FuelTypes);
                AddData(context, data.TransmissionTypes);
                AddData(context, data.EngineTypes);
                AddData(context, data.Engines);
                AddData(context, data.Extras);
            }

            return builder;
        }

        private static SeedDataModel GetData(string fileName)
        {
            SeedDataModel? result;

            using (StreamReader reader = new StreamReader(fileName))
            {
                string data = reader.ReadToEnd();
                result = JsonConvert.DeserializeObject<SeedDataModel>(data);
            }

            return result;
        }

        private static void AddData<T>(ApplicationDbContext context, T[] data)
            where T : class
        {
            if (context.Set<T>().Any())
                return;

            context.Set<T>().AddRange(data);
            context.SaveChanges();
        }
    }
}
