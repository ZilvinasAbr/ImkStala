using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImkStala.DataAccess.Entities
{
    public class Interior
    {

        public int InteriorId { get; set; }
        public Restaurant Restaurant { get; set; }
        public string InteriorPath { get; set; }

    }
}
