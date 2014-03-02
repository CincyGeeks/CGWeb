﻿using CincyGeeksWebsite.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CincyGeeksWebsite.Models.Account
{
    public class RegisterModel
    {
        [Required]
        [StringLength(56, ErrorMessage = "Your {0} must be no more than {2} characters long.")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(256, ErrorMessage = "Your {0} must be no more than {2} characters long.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [Compare("Email", ErrorMessage = "The email and confirmation email do not match.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Confirm Email Address")]
        public string ConfirmEmail { get; set; }

        [UIHint("Timezone")]
        public string Timezone { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(24, ErrorMessage = "Your {0} must be no more than {2} characters long.")]
        [Display(Name = "Phone Number (Optional)")]
        public string PhoneNumber { get; set; }

        [StringLength(128, ErrorMessage = "Your {0} must be no more than {2} characters long.")]
        [Display(Name = "Steam Community Name (Optional)")]
        public string SteamCommunityName { get; set; }

        [Required]
        [Display(Name = "Be listed on the [CG] directory page")]
        public bool ListInDirectory { get; set; }
    }
}