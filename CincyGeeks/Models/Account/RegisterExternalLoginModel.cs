using CincyGeeksWebsite.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CincyGeeksWebsite.Models.Account
{
    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

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

        [DataType(DataType.PhoneNumber)]
        [StringLength(24, ErrorMessage = "Your {0} must be no more than {2} characters long.")]
        [Display(Name = "Phone Number (Optional)")]
        public string PhoneNumber { get; set; }

        [UIHint("Timezone")]
        public string Timezone { get; set; }

        [StringLength(128, ErrorMessage = "Your {0} must be no more than {2} characters long.")]
        [Display(Name = "Steam Community Name (Optional)")]
        public string SteamCommunityName { get; set; }

        [Required]
        [Display(Name = "Be listed on the [CG] directory page")]
        public bool ListInDirectory { get; set; }

        public string ExternalLoginData { get; set; }
    }
}