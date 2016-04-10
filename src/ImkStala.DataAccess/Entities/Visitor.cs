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
        }

        public int VisitorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public List<Reservation> VisitorReservations { get; set; } 
    }
}
