using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code
{
    internal class Kurs
    {
        DateTime Datum {  get; set; }
        double Wert { get; set; }
        int Anzahlkurse {  get; set; }

        public Kurs(DateTime _datum, double _wert, int _anzahlkurse) { 
            Datum = _datum;
            Wert = _wert;
            Anzahlkurse = _anzahlkurse;
        }
    }
}