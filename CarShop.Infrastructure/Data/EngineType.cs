using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarShop.Infrastructure.Data
{
    public class EngineType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public int FuelTypeId { get; set; }

        [ForeignKey(nameof(FuelTypeId))]
        public FuelType FuelType { get; set; }
    }
}