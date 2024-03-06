using System.ComponentModel.DataAnnotations;

namespace ASWAQ.ViewModels
{
    public class AddProductViewModel
    {
        public int SectionID { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
