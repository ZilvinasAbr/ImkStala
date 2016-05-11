using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImkStala.Web.ViewModels.Manage
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Esamas slaptažodis yra privalomas")]
        [DataType(DataType.Password)]
        [Display(Name = "Esamas slaptažodis*")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Naujas slaptažodis yra privalomas")]
        [StringLength(100, ErrorMessage = "{0} turi būti bent {2} simbolių ilgio.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Naujas slaptažodis*")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Patvirtinkite slaptažodį*")]
        [Compare("NewPassword", ErrorMessage = "Slaptažodžiai nesutampa.")]
        public string ConfirmPassword { get; set; }
    }
}
