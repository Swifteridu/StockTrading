using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code
{
    using System;
    using System.Collections.Generic;

    public class Kunde
    {
        public string Name { get; set; }
        public string Vorname { get; set; }
        public double Budget { get; set; }
        public Portfolio Portfolio { get; set; }

        public Kunde(string name, string vorname, Portfolio portfolio)
        {
            Name = name;
            Vorname = vorname;
            Portfolio = portfolio;
        }
    }
}
