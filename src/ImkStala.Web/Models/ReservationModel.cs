using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImkStala.Web.Models
{
    public class ReservationModel
    {
        public int Id { get; set; }
        public DateTime ReservationStartDateTime { get; set; }
        public DateTime ReservationEndDateTime { get; set; }
    }
}
