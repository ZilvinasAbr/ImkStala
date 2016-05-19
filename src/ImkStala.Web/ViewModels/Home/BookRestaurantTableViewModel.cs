using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImkStala.DataAccess.Entities;

namespace ImkStala.Web.ViewModels.Home
{
    public class BookRestaurantTableViewModel
    {
        public int RestaurantId { get; set; }
        public int RestaurantTableSeats { get; set; }
        public string VisitorMessage { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public IEnumerable<string> Selected { get; set; }
    }
}
