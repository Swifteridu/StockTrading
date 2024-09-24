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

        public Bank(List<WertpapierPosten> wertpapierListe)
        {
            WertpapierListe = wertpapierListe;
        }

        public void WerpapierLaden()
        {
            WertpapierListe.Add(new WertpapierPosten(3, 200.0, new ETF("bsn", "us6383939", "edmf")));
            WertpapierListe.Add(new WertpapierPosten(3, 200.0, new ETF("lol", "unee939", "mfkf")));
            WertpapierListe.Add(new WertpapierPosten(3, 200.0, new ETF("huen", "us638ee444", "nama")));
        }

        public void WertpapierSpeichern()
        {
            Console.WriteLine("Wertpapier an die Bank verkauft: " + posten.Wertpapier.Name);
        }
    }
}
