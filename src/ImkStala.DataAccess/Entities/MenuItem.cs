using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImkStala.DataAccess.Entities
{

    public class MenuItem
    {
        //Navigation properties
        public Restaurant Restaurant { get; set; }
        public MenuItemType Type { get; set; }
        //Data of the entity
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
