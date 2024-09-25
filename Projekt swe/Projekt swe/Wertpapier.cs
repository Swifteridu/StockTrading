using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_swe
{
    public abstract class Wertpapier
    {
        public string Name { get; set; }
        public string ISIN { get; set; }
        public List<Kurs> KursListe { get; set; }

        public Wertpapier(string name, string isin)
        {
            Name = name;
            ISIN = isin;
            KursListe = new List<Kurs>();
        }

        // Füge eine Eigenschaft hinzu, die den letzten Kurswert zurückgibt
        public double AktuellerKurs
        {
            get
            {
                // Wenn KursListe leer ist, gib 0 zurück, andernfalls den letzten Kurswert
                return KursListe.Any() ? KursListe.Last().Wert : 0;
            }
        }
    }
}