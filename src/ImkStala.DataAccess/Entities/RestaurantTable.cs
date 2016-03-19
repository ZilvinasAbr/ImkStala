using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImkStala.DataAccess.Entities
{
    public class RestaurantTable
    {
        public int RestaurantTableId { get; set; }
        public int RestaurantTableSeats { get; set; }
        public ReservationCalendar ReservationCalendar { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
