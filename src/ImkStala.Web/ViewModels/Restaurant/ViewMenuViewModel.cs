using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImkStala.DataAccess.Entities;

namespace ImkStala.Web.ViewModels.Restaurant
{
    public class ViewMenuViewModel
    {
        public IEnumerable<IGrouping<MenuItemType, MenuItem>> Meals { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string SelectedMenuItemType { get; set; }
        public IEnumerable<MenuItemType> MenuItemTypes { get; set; }
        public string NewTypeName { get; set; }
    }
}
