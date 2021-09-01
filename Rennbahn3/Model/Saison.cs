using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rennbahn3.Model
{
    class Saison
    {
        public int SaisonID { get; set; }
        public string Name { get; set; }
        public List<Driver> Drivers { get; set; }
        public List<Race> Races { get; set; }
    }
}
