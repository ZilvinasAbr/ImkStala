using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Internal;

namespace ImkStala.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Reservation> Reservations { get; set; } 
        public DbSet<ReservationCalendar> ReservationCalendars { get; set; } 
        public DbSet<Table> Tables { get; set; } 
        public DbSet<Restaurant> Restaurants { get; set; } 
        public DbSet<User> UsersData { get; set; } 
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
