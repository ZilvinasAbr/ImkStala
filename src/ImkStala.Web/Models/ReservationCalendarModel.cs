using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImkStala.Web.Models
{
    public class ReservationCalendarModel
    {
        public int Id { get; set; }
        public IList<ReservationModel> Reservations { get; set; }  
    }
}
