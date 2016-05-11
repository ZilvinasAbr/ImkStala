﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImkStala.DataAccess.Entities;

namespace ImkStala.Web.ViewModels.Restaurant
{
    public class ViewTablesViewModel
    {
        public IEnumerable<RestaurantTable> Tables { get; set; }
        public int TableSeats { get; set; }
    }
}
