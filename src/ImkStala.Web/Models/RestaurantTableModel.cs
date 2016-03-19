using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImkStala.DataAccess.Entities;

namespace ImkStala.Web.Models
{
    public class RestaurantTableModel
    {
        public int RestaurantTableSeats { get; set; }
        public ReservationCalendar ReservationCalendar { get; set; }
    }
}
