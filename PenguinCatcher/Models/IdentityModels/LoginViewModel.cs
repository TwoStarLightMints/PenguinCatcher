﻿using System.ComponentModel.DataAnnotations;

namespace PenguinCatcher.Models.IdentityModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter a user name")]
        [StringLength(255)]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = " Please enter a password")]
        [StringLength(255)]
        public string Password { get; set; } = string.Empty;

        public string ReturnUrl { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}
