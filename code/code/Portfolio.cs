using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace code
{
    public class Portfolio
    {
        public List<WertpapierPosten> WertpapierListe { get; set; }

        public Portfolio(List<WertpapierPosten> wertpapierListe)
        {
            WertpapierListe = wertpapierListe;
        }

        public List<WertpapierPosten> PortfolioLaden(string name, string vorname)
        {
            List<WertpapierPosten> liste = new List<WertpapierPosten>();
            string datei = "Personendaten.txt";

            // Datei öffnen
            using (StreamReader str = new StreamReader(datei))
            {
                string line;

                // Datei zeilenweise lesen
                while ((line = str.ReadLine()) != null)
                {
                    string[] teil = line.Split(';');
                    string vornameTest = teil[0];
                    string nameTest = teil[1];

                    // Prüfen, ob der Name übereinstimmt
                    if (vorname.Equals(vornameTest) || name.Equals(nameTest))
                    {
                        string hallo;
                        // Weitere Zeilen lesen
                        while ((hallo = str.ReadLine()) != null && !string.IsNullOrWhiteSpace(hallo))
                        {
                            string[] teil1 = hallo.Split(';');
                            int anzahl = int.Parse(teil1[0]);
                            double wert = double.Parse(teil1[1]);
                            string typ = teil1[2];
                            string Tname = teil1[3];
                            string isinNummer = teil1[4];
                            // Je nach Wertpapier-Typ handeln
                            switch (typ)
                            {
                                case "ETF":
                                    string basis = teil1[5];
                                    liste.Add(new WertpapierPosten(anzahl, wert, new ETF(Tname, isinNummer, basis)));
                                    break;

                                case "Optionsschein":
                                    DateTime datum = DateTime.Parse(teil1[5]);
                                    string Bezeichner = teil1[6];
                                    liste.Add(new WertpapierPosten(anzahl, wert, new Optionsschein(Tname, isinNummer, datum, Bezeichner)));
                                    break;

                                case "Aktie":
                                    string kuerzel = teil1[5];
                                    double devidende = double.Parse(teil1[6]);
                                    liste.Add(new WertpapierPosten(anzahl, wert, new Aktie(Tname, isinNummer, kuerzel, devidende)));
                                    break;

                                case "Anleihen":
                                    DateTime laufzeit = DateTime.Parse(teil1[5]);
                                    double couponwert = double.Parse(teil1[6]);
                                    liste.Add(new WertpapierPosten(anzahl, wert, new Anleihen(Tname, isinNummer, laufzeit, couponwert)));
                                    break;
                            }
                        }
                    }
                }
            }
            return liste;
        }
    }
}
