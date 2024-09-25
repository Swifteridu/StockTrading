using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_swe
{
    public class Anleihe : Wertpapier
    {
        public DateTime Laufzeit { get; set; }
        public double Couponwert { get; set; }

        public Anleihe(string name, string isin, DateTime laufzeit, double kouponwert) : base(name, isin)
        {
            Laufzeit = laufzeit;
            Couponwert = kouponwert;
        }
    }
}
