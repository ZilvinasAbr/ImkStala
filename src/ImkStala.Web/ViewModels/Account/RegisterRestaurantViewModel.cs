using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImkStala.Web.ViewModels.Account
{
    public class RegisterRestaurantViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name ="Restaurant name")]
        public string RestaurantName { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Phone]
        [StringLength(12, ErrorMessage = "The {0} field must be {2} characters long.", MinimumLength = 12)]
        [RegularExpression("^(\\+370)[0-9]+", ErrorMessage = "The {0} must be a valid phone number (must begin with '+370')")]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [RegularExpression("^(https?://)(.+)?", ErrorMessage = "The {0} must be a valid website adress")]
        [Display(Name = "Website")]
        public string Website { get; set; }
    }
}