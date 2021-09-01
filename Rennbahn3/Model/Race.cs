using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rennbahn3.Model
{
    class Race
    {
        public int RaceID { get; set; }
        public string Name { get; set; }
        public Saison Saison { get; set; }
        public List<Result> Results { get; set; }
    }
}
