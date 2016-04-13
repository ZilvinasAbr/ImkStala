using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace ImkStala.DataAccess.Entities
{
    public class RestaurantTable
    {
        public RestaurantTable()
        {
            Reservations = new List<Reservation>();
        }

        //Navigation properties
        public Restaurant Restaurant { get; set; }
        public ICollection<Reservation> Reservations { get; set; }

        //Data properties

        public int RestaurantTableId { get; set; }
        public int RestaurantTableSeats { get; set; }


        public bool CanAddReservation(Reservation reservation)
        {
            //return true; //for testing, it is possible to add intersecting reservations
            List<Reservation> reservationsList = (List<Reservation>) Reservations;
            if (reservationsList.Exists(r => DoReservationsIntersect(r, reservation)))
            {
                return false;
            }
            return true;
        }

        private static bool DoReservationsIntersect(Reservation first, Reservation second)
        {
            if (first.ReservationEndDateTime < second.ReservationStartDateTime ||
                second.ReservationEndDateTime < first.ReservationStartDateTime)
            {
                return false;
            }

            return true;
        }

    }
}
