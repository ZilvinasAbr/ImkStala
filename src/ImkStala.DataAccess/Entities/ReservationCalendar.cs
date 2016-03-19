using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImkStala.DataAccess.Entities
{
    public class ReservationCalendar
    {
        public int ReservationCalendarId { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
