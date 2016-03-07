using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImkStala.Models
{
    public class ReservationCalendar
    {
        public int Id { get; set; }
        private List<Reservation> Reservations { get; set; }
    }
}
