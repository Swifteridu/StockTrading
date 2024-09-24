using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_swe
{
    public class Kurs
    {
        public DateTime Datum { get; set; }
        public double Wert { get; set; }

        public Kurs(DateTime datum, double wert)
        {
            Datum = datum;
            Wert = wert;
        }
    }
}
