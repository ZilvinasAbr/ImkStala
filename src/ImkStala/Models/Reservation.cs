using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImkStala.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime ReservationStartDateTime { get; set; }
        public DateTime ReservationEndDateTime { get; set; }
    }
}
