using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace CarShop.Infrastructure.Data
{
    public class Car
    {
        [Key]
        public Guid Id { get; set; } = new Guid();

        [Required]
        public int BrandId { get; set; }
        [ForeignKey(nameof(BrandId))]
        public Brand Brand { get; set; }

        [Required]
        [StringLength(50)]
        public string Model { get; set; }

        [Required]
        public int ReleaseYear { get; set; }

        [Required]
        public int Height { get; set; }

        [Required]
        public int Width { get; set; }

        [Required]
        public int Length { get; set; }

        [Required]
        public int Weight { get; set; }

        [Required]
        public int CoupeTypeId { get; set; }
        [ForeignKey(nameof(CoupeTypeId))]
        public CoupeType CoupeType { get; set; }

        [Required]
        public int DoorConfigId { get; set; }
        [ForeignKey(nameof(DoorConfigId))]
        public DoorConfig DoorConfig { get; set; }

        [Required]
        public double CrashProtectionLevel { get; set; }

        [Required]
        public double FuelConsumption { get; set; }

        [Required]
        public int EngineId { get; set; }
        [ForeignKey(nameof(EngineId))]
        public Engine Engine { get; set; }

        [Required]
        public int DriveTrainTypeId { get; set; }
        [ForeignKey(nameof(DriveTrainTypeId))]
        public DriveTrainType DriveTrainType { get; set; }

        public decimal Price { get; set; }

        public IList<Image> Images { get; set; } = new List<Image>();
    }
}
