﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImkStala.Web.ViewModels.Account
{
    public class RegisterVisitorViewModel
    {
        [Required(ErrorMessage = "Elektroninis paštas yra privalomas")]
        [EmailAddress(ErrorMessage = "{0} turi būti teisingo formato.")]
        [Display(Name = "Elektroninis paštas*")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Slaptažodis yra privalomas")]
        [StringLength(100, ErrorMessage = "{0} turi būti bent {2} simbolių ilgio.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Slaptažodis*")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Patvirtinkite slaptažodį*")]
        [Compare("Password", ErrorMessage = "Slaptažodžiai nesutampa!")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Vardas")]
        public string FirstName { get; set; }

        [Display(Name = "Pavardė")]
        public string LastName { get; set; }

        [Phone]
        [StringLength(12, ErrorMessage = "{0} turi būti {2} simbolių ilgio.", MinimumLength = 12)]
        [RegularExpression("^(\\+370)[0-9]+", ErrorMessage = "{0} turi būti teisingas (turi prasidėti su '+370')")]
        [Display(Name = "Telefono numeris")]
        public string PhoneNumber { get; set; }
    }
}
