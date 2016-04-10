using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImkStala.DataAccess.Entities
{
    public class ReservationCalendar
    {
        public ReservationCalendar()
        {
            Reservations = new List<Reservation>();
        }

        public int ReservationCalendarId { get; set; }
        public List<Reservation> Reservations { get; set; }

        public bool CanAddReservation(Reservation reservation)
        {
            return true; //for testing, it is possible to add intersecting reservations
            if (Reservations.Exists(r => !DoReservationsIntersect(r, reservation)))
            {
                return true;
            }
            return false;
        }

        private static bool DoReservationsIntersect(Reservation first, Reservation second)
        {
            if (first.ReservationStartDateTime > second.ReservationEndDateTime ||
                second.ReservationStartDateTime > first.ReservationEndDateTime)
                return true;
            return false;
        }
    }
}
