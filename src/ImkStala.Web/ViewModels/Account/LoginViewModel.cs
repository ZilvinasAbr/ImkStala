using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImkStala.Web.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Elektroninis paštas yra privalomas")]
        [EmailAddress(ErrorMessage = "{0} turi būti teisingo formato.")]
        [Display(Name = "El. paštas")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Slaptažodis yra privalomas")]
        [DataType(DataType.Password)]
        [Display(Name = "Slaptažodis")]
        public string Password { get; set; }

        [Display(Name = "Prisiminti")]
        public bool RememberMe { get; set; }
    }
}
