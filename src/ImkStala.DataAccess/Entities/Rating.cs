using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImkStala.DataAccess.Entities
{
    public class Rating
    {

        public int RatingId { get; set; }
        public Restaurant Restaurant { get; set; }
        public Visitor Visitor { get; set; }
        public int RatingValue { get; set; }

    }
}
