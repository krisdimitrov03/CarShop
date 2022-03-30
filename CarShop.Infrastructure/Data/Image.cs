using System.ComponentModel.DataAnnotations;

namespace CarShop.Infrastructure.Data
{
    public class Image
    {
        [Key]
        public Guid Id { get; set; } = new Guid();

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public bool IsProfile { get; set; }
    }
}
