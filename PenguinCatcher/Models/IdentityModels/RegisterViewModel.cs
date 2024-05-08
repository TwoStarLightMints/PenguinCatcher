using System.ComponentModel.DataAnnotations;

namespace PenguinCatcher.Models.IdentityModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter a user name")]
        [StringLength(255)]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a password")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public bool IsAdmin { get; set; } = false;

        public string ReturnUrl { get; set; } = string.Empty;
    }
}
