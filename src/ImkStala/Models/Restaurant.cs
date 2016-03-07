using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImkStala.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string VATcode { get; set; } //PVM moketojo kodas
        public string Manager { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; } //telefono numeris
        public string Email { get; set; }
        public string Website { get; set; }
        public string Workhours { get; set; }
        public double Rating { get; set; }
        public List<Table> Tables { get; set; }

        //kokiu formatu turėtų būti registracijos data?
        //string, ar kiekvienas datos laukas atskirai?
        //Atsakymas: DateTime tipas manau tam tiktu
        public DateTime RegistrationDate { get; set; }
    }
}
