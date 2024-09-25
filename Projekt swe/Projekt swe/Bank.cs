using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_swe
{
    public class Bank
    {
        public List<Wertpapier> WertpapierListe { get; set; }

        public Bank(List<Wertpapier> wertpapierListe)
        {
            WertpapierListe = wertpapierListe;
        }

        public void Kaufen(Kunde kunde, Wertpapier wertpapier, int anzahl)
        {
            var posten = new WertpapierPosten(anzahl, wertpapier.KursListe[1].Wert, wertpapier);
            kunde.Kaufen(posten);
        }

        public void Verkaufen(Kunde kunde, Wertpapier wertpapier, int anzahl)
        {
            kunde.Verkaufen(wertpapier, anzahl);
        }
    }
}
