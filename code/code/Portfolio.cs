using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code
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
            Console.WriteLine("Wertpapier gekauft: " + posten.Wertpapier.Name);
        }

        public void Verkaufen(WertpapierPosten posten)
        {
            WertpapierListe.Remove(posten);
            Console.WriteLine("Wertpapier verkauft: " + posten.Wertpapier.Name);
        }
    }
}
