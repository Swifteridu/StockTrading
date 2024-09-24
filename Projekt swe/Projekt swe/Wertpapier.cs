using System;
using System.Collections.Generic;

namespace Projekt_swe
{
    public abstract class Wertpapier
    {
        public string Name { get; set; }
        public string ISIN_Nummer { get; set; }
        public List<Kurs> kursListe { get; set; }

        protected Wertpapier(string name, string isin_nummer)
        {
            Name = name;
            ISIN_Nummer = isin_nummer;
            kursListe = new List<Kurs>();
        }
    }
}