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
        public string ISIN_Nummer { get; set; }
        public List<Kurs> KursListe { get; set; }

        public Wertpapier(string name, string isin)
        {
            Name = name;
            ISIN_Nummer = isin;
            KursListe = new List<Kurs>();
        }
    }
}
