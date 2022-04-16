using CarShop.Infrastructure.Seeders.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarShop.Infrastructure.Seeders
{
	public class Seeder<T> : ISeeder<T> 
		where T : class
	{
		public IEnumerable<T> GetData()
		{
			var json = File.ReadAllText($"../Carshop.Infrastructure/Seeders/Data/{typeof(T).Name.ToLower()}s.json");
			var entities = JsonSerializer.Deserialize<List<T>>(json);

			return entities;
		}
	}
}
