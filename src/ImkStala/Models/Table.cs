using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImkStala.Models
{
    public class Table
    {
        public int Id { get; set; }
        public int TableSeats { get; set; }
        public ReservationCalendar ReservationCalendar { get; set; }
    }
}
