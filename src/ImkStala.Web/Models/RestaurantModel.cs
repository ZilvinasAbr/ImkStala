using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImkStala.DataAccess.Entities;

namespace ImkStala.Web.Models
{
    public class RestaurantModel
    {
        public string RestaurantName { get; set; }
        public string VatCode { get; set; } // (lt. PVM moketojo kodas)
        public string Manager { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Workhours { get; set; }
        public double Rating { get; set; }
        public DateTime RegistrationDate { get; set; }
        public List<RestaurantTableModel> RestaurantTables { get; set; } 
    }
}
