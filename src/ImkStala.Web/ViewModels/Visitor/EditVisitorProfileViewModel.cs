using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImkStala.Web.ViewModels.Visitor
{
    public class EditVisitorProfileViewModel
    {

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
