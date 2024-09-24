using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_swe
{
    public class WertpapierPosten
    {
        public Wertpapier Wertpapier { get; set; }
        public int Anzahl { get; set; }

        public WertpapierPosten(Wertpapier wertpapier, int anzahl)
        {
            Wertpapier = wertpapier;
            Anzahl = anzahl;
        }
    }
}
