using System.ComponentModel.DataAnnotations;

namespace ASWAQ.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string CurentPassword { get; set; }
        
        [Required(ErrorMessage = "Please Enter Password")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Password Must Be Less Than 15 Characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
