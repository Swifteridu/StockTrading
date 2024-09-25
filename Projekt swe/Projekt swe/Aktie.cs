using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_swe
{
    public class Aktie : Wertpapier
    {
        public string Kuerzel { get; set; }
        public double Dividende { get; set; }

        public Aktie(string name, string isin, string kuerzel, double dividende) : base(name, isin)
        {
            Kuerzel = kuerzel;
            Dividende = dividende;
        }
    }
}
