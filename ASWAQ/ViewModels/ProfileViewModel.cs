using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ASWAQ.ViewModels
{
    public class ProfileViewModel
    {
        [Required(ErrorMessage = "Please Enter User Name")]
        [RegularExpression(@"^[a-zA-Z0-9_-]{5,}$", ErrorMessage = "Please Enter A Valid User Name.")]
        [Remote("UserNameValid", "Account", ErrorMessage = "This User Name Is Already Exists")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Please Enter The First Name")]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter The Last Name")]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}", ErrorMessage = "Please Enter A Valid Email Address.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please Enter The Phone Number")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Phone Number Must Be 11 Digits")]
        public string? PhoneNumber { get; set; }
        
    }
}
