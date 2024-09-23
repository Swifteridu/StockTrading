using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code
{
    public class Bank
    {
        public List<Wertpapier> WertpapierListe { get; set; }

        public Bank(List<Wertpapier> wertpapierListe)
        {
            WertpapierListe = wertpapierListe;
        }

        public void Kaufen(WertpapierPosten posten)
        {
            Console.WriteLine("Wertpapier von der Bank gekauft: " + posten.Wertpapier.Name);
        }

        public void Verkaufen(WertpapierPosten posten)
        {
            Console.WriteLine("Wertpapier an die Bank verkauft: " + posten.Wertpapier.Name);
        }
    }
}
