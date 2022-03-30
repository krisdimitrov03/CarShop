using System.ComponentModel.DataAnnotations;

namespace CarShop.Infrastructure.Data
{
    public class Color
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}