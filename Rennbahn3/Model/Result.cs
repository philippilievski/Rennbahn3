using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rennbahn3.Model
{
    class Result
    {
        public int ResultID { get; set; }
        public Race Race { get; set; }
        public Driver Driver { get; set; }
        public int Position { get; set; }
    }
}
