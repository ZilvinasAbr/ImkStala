using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ImkStala.DataAccess.Entities;

namespace ImkStala.Web.ViewModels.Restaurant
{
    public class EditRestaurantProfileViewModel
    {

        [Display(Name = "Restorano pavadinimas")]
        public string RestaurantName { get; set; }

        [Display(Name = "Adresas")]
        public string Address { get; set; }

        [Phone]
        [StringLength(12, ErrorMessage = "{0} turi būti {2} simbolių ilgio.", MinimumLength = 12)]
        [RegularExpression("^(\\+370)[0-9]+", ErrorMessage = "{0} turi būti teisingas (turi prasidėti su '+370')")]
        [Display(Name = "Telefono numeris")]
        public string PhoneNumber { get; set; }

        [RegularExpression("^(https?://)(.+)?", ErrorMessage = "{0} turi būti teisingas")]
        [Display(Name = "Restorano tinklalapis")]
        public string Website { get; set; }

        [Display(Name = "Aprašymas")]
        public string Description { get; set; }

        [Display(Name = "Logotipas")]
        public string LogoPath { get; set; }

        [Display(Name = "Interjeras")]
        public ICollection<Interior> Interiors { get; set; }

    }
}
