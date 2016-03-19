using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImkStala.DataAccess.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace ImkStala.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    { 
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationCalendar> ReservationCalendars { get; set; }
        public DbSet<RestaurantTable> RestaurantTables { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }      
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}