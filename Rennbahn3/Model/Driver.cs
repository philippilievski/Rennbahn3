using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rennbahn3.Model
{
    class Driver
    {
        public int DriverID { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public Saison Saison { get; set; }
        public Team Team { get; set; }
        public List<Result> Results { get; set; }
    }
}
