using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code
{
    public class Aktie : Wertpapier
    {
        public string Kuerzel { get; set; }
        public double Dividende { get; set; }

        public Aktie(string name, string isinNummer, string kuerzel, double dividente)
            : base(name, isinNummer)
        {
            Kuerzel = kuerzel;
            Dividende = dividente;
        }
    }
}
