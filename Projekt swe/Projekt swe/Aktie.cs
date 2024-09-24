using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_swe
{
    public class Aktie : Wertpapier
    {
        public string Kürzel { get; set; }
        public double Dividende { get; set; }

        public Aktie(string name, string isin_nummer, string kuerzel, double dividende)
            : base(name, isin_nummer)
        {
            Kürzel = kuerzel;
            Dividende = dividende;
        }
    }
}
