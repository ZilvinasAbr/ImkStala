using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImkStala.DataAccess.Entities;

namespace ImkStala.Web.ViewModels.Restaurant
{
    public class ViewMenuViewModel
    {
        public IEnumerable<MenuItem> Meals { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
