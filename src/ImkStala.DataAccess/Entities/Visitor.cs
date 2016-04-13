using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImkStala.DataAccess.Entities
{
    public class Visitor
    {
        public Visitor()
        {
            VisitorReservations = new List<Reservation>();
            Favorites = new List<Restaurant>();
            Ratings = new List<Rating>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        //Navigation properties
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Reservation> VisitorReservations { get; set; } 
        public ICollection<Restaurant> Favorites { get; set; }
        public ICollection<Rating> Ratings { get; set; }
    }
}
