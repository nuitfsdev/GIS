﻿using System.ComponentModel.DataAnnotations;

namespace GIS.ViewModels.Account
{
    public class SignUp
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}