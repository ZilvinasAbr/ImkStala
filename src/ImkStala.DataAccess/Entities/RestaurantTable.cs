using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImkStala.DataAccess.Entities
{
    public class RestaurantTable
    {
        public RestaurantTable()
        {
            ReservationCalendar = new ReservationCalendar();
        }

        //Navigation properties
        public Restaurant Restaurant { get; set; }
        public ReservationCalendar ReservationCalendar { get; set; }

        //Data properties

        public int RestaurantTableId { get; set; }
        public int RestaurantTableSeats { get; set; }
        
        
    }
}
