using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code
{
    public class Anleihen : Wertpapier
    {
        public DateTime Laufzeit { get; set; }
        public double Kouponwert { get; set; }

        public Anleihen(string name, string isinNummer, DateTime laufzeit, double kouponwert)
            : base(name, isinNummer)
        {
            Laufzeit = laufzeit;
            Kouponwert = kouponwert;
        }
    }
}
