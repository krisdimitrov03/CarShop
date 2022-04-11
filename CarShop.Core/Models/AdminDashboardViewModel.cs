using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Core.Models
{
    public class AdminDashboardViewModel
    {
        public IEnumerable<OrderListViewModel> Orders { get; set; }

        public IEnumerable<UserListViewModel> Employees { get; set; }

        public IEnumerable<UserListViewModel> Clients { get; set; }

        public decimal TotalIncome { get; set; }

        public string MostSaledCar { get; set; }

        public string CarImageUrl { get; set; }

        public string CountOfSalesOfCar { get; set; }
    }
}
