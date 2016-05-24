using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImkStala.DataAccess.Entities;
using System.ComponentModel.DataAnnotations;

namespace ImkStala.Web.ViewModels.Restaurant
{
    public class ViewMenuViewModel
    {
        public IEnumerable<IGrouping<MenuItemType, MenuItem>> Meals { get; set; }

        [Display(Name = "Pavadinimas")]
        [Required(ErrorMessage = "Pavadinimas yra privalomas")]
        public string Name { get; set; }

        [Display(Name = "Kaina")]
        [Required(ErrorMessage = "Kaina yra privaloma")]
        [StringLength(10, ErrorMessage = "Leidžiama tik 10 simbolių")]
        [RegularExpression("(^\\d+$|^\\d+\\,\\d$|^\\d+\\,\\d\\d$)", ErrorMessage = "Įveskite tinkamą kainą (x arba x,x arba x,xx)")]
        public string Price { get; set; }

        [Display(Name = "Patiekalo tipas")]
        public string SelectedMenuItemType { get; set; }

        public IEnumerable<MenuItemType> MenuItemTypes { get; set; }

        [Display(Name = "Naujas patiekalo tipas")]
        [Required(ErrorMessage = "Tipas yra privalomas")]
        public string NewTypeName { get; set; }
    }
}
