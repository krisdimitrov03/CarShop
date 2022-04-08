using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarShop.Infrastructure.Data
{
    public class OrdersExtra
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid CarId { get; set; }
        [ForeignKey(nameof(CarId))]
        public Car Car { get; set; }

        [Required]
        public int ExtraId { get; set; }
        [ForeignKey(nameof(ExtraId))]
        public Extra Extra { get; set; }
    }
}
