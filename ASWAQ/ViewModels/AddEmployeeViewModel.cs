using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace ASWAQ.ViewModels
{
    public class AddEmployeeViewModel
    {
        [Required(ErrorMessage = "Please Enter User Name")]
        [RegularExpression(@"^[a-zA-Z0-9_-]{3,}$", ErrorMessage = "Please Enter A Valid User Name.")]
        [Remote("UserNameValid", "Account", ErrorMessage = "This User Name Is Already Exists")]
        public string? UserName { get; set; }


        [Required]
        public List<int>? SectionsId { get; set; }

        [Required]
        [Range(2000, 20000)]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Password Must Be Less Than 15 Characters.")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm Password Not Match With Password")]
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; }
        

    }
}
