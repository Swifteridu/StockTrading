using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code
{
    public abstract class Wertpapier
    {
        public string Name { get; set; }
        public string ISIN_Nummer { get; set; }

        public Wertpapier(string name, string isinNummer)
        {
            Name = name;
            ISIN_Nummer = isinNummer;
        }
    }
}
