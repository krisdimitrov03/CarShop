using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarShop.Infrastructure.Data
{
    public class OrdersExtra
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }

        [Required]
        public int ExtraId { get; set; }
        [ForeignKey(nameof(ExtraId))]
        public Extra Extra { get; set; }
    }
}
