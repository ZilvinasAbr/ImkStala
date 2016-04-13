using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImkStala.DataAccess.Entities;

namespace ImkStala.Web.Models
{
    public class RestaurantTableModel
    {
        public int Id { get; set; }
        public int RestaurantTableSeats { get; set; }
        public ICollection<ReservationModel> Reservations { get; set; } 
    }
}
