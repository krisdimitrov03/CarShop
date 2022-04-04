using CarShop.Infrastructure.Data.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Infrastructure.Data
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; } = new Guid();

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        [Required]
        public Guid CarId { get; set; }

        [ForeignKey(nameof(CarId))]
        public Car Car { get; set; }

        [Required]
        public decimal TotalPrice 
        { 
            get { return this.Car.Price; } 
        }
    }
}
