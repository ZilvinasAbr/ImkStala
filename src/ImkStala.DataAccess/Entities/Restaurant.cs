﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImkStala.DataAccess.Entities
{
    public class Restaurant
    {
        public Restaurant()
        {
            RestaurantTables = new List<RestaurantTable>();
            Meals = new List<MenuItem>();
            Ratings = new List<Rating>();
            Interiors = new List<Interior>();
        }

        public int Id { get; set; }
        public string RestaurantName { get; set; }
        public string VatCode { get; set; } //PVM moketojo kodas
        public string Manager { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; } //telefono numeris
        public string Email { get; set; }
        public string Website { get; set; }
        public string Workhours { get; set; }
        public double Rating { get; set; }
        public int RateAmount { get; set; }
        public string Description { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string LogoPath { get; set; }

        //Navigation properties
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<RestaurantTable> RestaurantTables { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<MenuItem> Meals { get; set; }
        public ICollection<Interior> Interiors { get; set; }
    }
}
