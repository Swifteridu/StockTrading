using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code
{
    public class WertpapierPosten
    {
        public int Anzahl { get; set; }
        public double Preis { get; set; }
        public Wertpapier Wertpapier { get; set; }

        public WertpapierPosten(int anzahl, double preis, Wertpapier wertpapier)
        {
            Anzahl = anzahl;
            Preis = preis;
            Wertpapier = wertpapier;
        }
    }
}
