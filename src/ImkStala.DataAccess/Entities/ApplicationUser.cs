using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ImkStala.DataAccess.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public Visitor VisitorData { get; set; }
        public Restaurant RestaurantData { get; set; }
    }
}
