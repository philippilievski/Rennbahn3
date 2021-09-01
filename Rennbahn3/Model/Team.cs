using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rennbahn3.Model
{
    class Team
    {
        public int TeamID { get; set; }
        public string Name { get; set; }
        public List<Driver> Drivers { get; set; }
    }
}
