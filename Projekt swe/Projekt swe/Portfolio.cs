using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_swe
{
    public class Portfolio
    {
        public string Name { get; set; }
        public Dictionary<string, WertpapierPosten> WertpapierPositionen { get; set; }

        public Portfolio(string name)
        {
            Name = name;
            WertpapierPositionen = new Dictionary<string, WertpapierPosten>();
        }

        public void FügeWertpapierHinzu(Wertpapier wertpapier, int anzahl)
        {
            if (WertpapierPositionen.ContainsKey(wertpapier.ISIN_Nummer))
            {
                WertpapierPositionen[wertpapier.ISIN_Nummer].Anzahl += anzahl;
            }
            else
            {
                WertpapierPositionen[wertpapier.ISIN_Nummer] = new WertpapierPosten(wertpapier, anzahl);
            }
        }

        public void EntferneWertpapier(Wertpapier wertpapier, int anzahl)
        {
            if (WertpapierPositionen.ContainsKey(wertpapier.ISIN_Nummer))
            {
                WertpapierPositionen[wertpapier.ISIN_Nummer].Anzahl -= anzahl;
                if (WertpapierPositionen[wertpapier.ISIN_Nummer].Anzahl <= 0)
                {
                    WertpapierPositionen.Remove(wertpapier.ISIN_Nummer);
                }
            }
        }

        public double BerechnePortfolioWert()
        {
            double gesamtwert = 0;
            foreach (var posten in WertpapierPositionen.Values)
            {
                if (posten.Wertpapier.kursListe.Count > 0)
                {
                    gesamtwert += posten.Wertpapier.kursListe[0].Wert * posten.Anzahl;
                }
            }
            return gesamtwert;
        }
    }
}
