using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

// 
namespace ASWAQ.ViewModels
{
    public class LoginViewModel
    {        
        [Required(ErrorMessage ="Please Enter UserName")]
        [Display(Name ="User Name")]
        public string? UserName { get; set; }

        [Required(ErrorMessage ="Please Enter Password")]
        [DataType(DataType.Password)]        
        public string? Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
