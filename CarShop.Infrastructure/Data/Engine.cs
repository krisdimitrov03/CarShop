using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarShop.Infrastructure.Data
{
    public class Engine
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Displacement { get; set; }
        
        [Required]
        public int EngineTypeId { get; set; }
        [ForeignKey(nameof(EngineTypeId))]
        public EngineType EngineType { get; set; }

        [Required]
        public int HorsePower { get; set; }

        [Required]
        public int CilinderConfig { get; set; }

        [Required]
        public bool Turbo { get; set; }
    }
}