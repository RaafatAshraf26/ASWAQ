using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ASWAQ.ViewModels
{
    public class EditEmployeeViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Please Enter User Name")]
        [RegularExpression(@"^[a-zA-Z0-9_-]{3,}$", ErrorMessage = "Please Enter A Valid User Name.")]
        [Remote("UserNameValid", "Account" , AdditionalFields = "Id" , ErrorMessage = "This User Name Is Already Exists")]
        public string? UserName { get; set; }

        [Required]
        public List<int> SectionsId { get; set; } = new();

        [Required]
        [Range(2000, 20000)]
        public decimal Salary { get; set; }
    }
}
