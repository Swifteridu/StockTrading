using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_swe
{
    public class Kunde
    {
        public string Name { get; set; }
        public string Vorname { get; set; }
        public Portfolio Portfolio { get; set; }
        public double Budget { get; set; }

        public Kunde(string name, string vorname, Portfolio portfolio, double budget)
        {
            Name = name;
            Vorname = vorname;
            Portfolio = portfolio;
            Budget = budget;
        }

        public void Kaufen(WertpapierPosten posten)
        {
            if (Budget >= posten.Preis * posten.Anzahl)
            {
                Portfolio.Kaufen(posten);
                Budget -= posten.Preis * posten.Anzahl;
            }
            else
            {
                Console.WriteLine("Nicht genug Budget.");
            }
        }

        public void Verkaufen(Wertpapier wertpapier, int anzahl)
        {
            Portfolio.Verkaufen(wertpapier, anzahl);
        }
    }
}
