using System.ComponentModel.DataAnnotations;

namespace ASWAQ.ViewModels
{
    public class AddSectionViewModel
    {
        [Required]
        public string? Name { get; set; }

        [Required]        
        public IFormFile Image { get; set; }
    }
}
