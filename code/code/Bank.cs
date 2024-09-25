using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code
{
    public class Bank
    {
        public List<WertpapierPosten> WertpapierListe { get; set; }

        public Bank()
        {
            WertpapierListe.Add(new WertpapierPosten(1, 50, new ETF("Apple", "us29939939", "APL")));
            WertpapierListe.Add(new WertpapierPosten(1, 50, new Aktie("Microsoft", "us25364893", "MCS", 23)));
            WertpapierListe.Add(new WertpapierPosten(1, 50, new Anleihen("Steam", "us29425633",new DateTime(20-12-2025), 45)));
            WertpapierListe.Add(new WertpapierPosten(1, 50, new Optionsschein("BMW", "us29435562", new DateTime(23-07-2026),"BMW-Schein"));
        }
    }
}
