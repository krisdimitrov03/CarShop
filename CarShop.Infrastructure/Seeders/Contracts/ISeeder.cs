using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Infrastructure.Seeders.Contracts
{
	public interface ISeeder<T> where T :class
	{
		IEnumerable<T> GetData();
	}
}
