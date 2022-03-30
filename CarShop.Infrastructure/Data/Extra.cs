using System.ComponentModel.DataAnnotations;

namespace CarShop.Infrastructure.Data
{
    public class Extra
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
