﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImkStala.DataAccess.Entities
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string RestaurantName { get; set; }
        public string VatCode { get; set; } //PVM moketojo kodas
        public string Manager { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; } //telefono numeris
        public string Email { get; set; }
        public string Website { get; set; }
        public string Workhours { get; set; }
        public double Rating { get; set; }
        public List<RestaurantTable> RestaurantTables { get; set; }
        public DateTime RegistrationDate { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
