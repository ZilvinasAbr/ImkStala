using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImkStala.DataAccess.Entities
{
    public class Reservation
    {
        //Navigation properties
        public Restaurant Restaurant { get; set; }
        public Visitor Visitor { get; set; }
        public RestaurantTable RestaurantTable { get; set; }

        //Data of the entity
        public int ReservationId { get; set; }
        public DateTime ReservationStartDateTime { get; set; }
        public DateTime ReservationEndDateTime { get; set; }
        public string VisitorMessage { get; set; }
        
    }
}
