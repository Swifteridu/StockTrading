using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_swe
{
    public class Portfolio
    {
        public List<WertpapierPosten> WertpapierListe { get; set; }

        public Portfolio(List<WertpapierPosten> wertpapierListe)
        {
            WertpapierListe = wertpapierListe;
        }

        public void Kaufen(WertpapierPosten posten)
        {
            WertpapierListe.Add(posten);
            Console.WriteLine($"Gekauft: {posten.Anzahl}x {posten.Wertpapier.Name} zu {posten.Preis} pro Stück");
        }

        public void Verkaufen(Wertpapier wertpapier, int anzahl)
        {
            var posten = WertpapierListe.Find(p => p.Wertpapier == wertpapier);
            if (posten != null && posten.Anzahl >= anzahl)
            {
                posten.Anzahl -= anzahl;
                if (posten.Anzahl == 0) WertpapierListe.Remove(posten);
                Console.WriteLine($"Verkauft: {anzahl}x {wertpapier.Name}");
            }
            else
            {
                Console.WriteLine("Verkauf nicht möglich.");
            }
        }
    }
}
