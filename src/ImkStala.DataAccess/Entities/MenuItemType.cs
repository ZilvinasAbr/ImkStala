using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImkStala.DataAccess.Entities
{
    public class MenuItemType
    {
        //Navigation properties
        public Restaurant Restaurant { get; set; }

        //Data of the entity
        public int Id { get; set; }
        public string TypeName { get; set; }
    }
}
