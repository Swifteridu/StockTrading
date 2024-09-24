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
            StreamReader str = new StreamReader(datei);
            string hallo = "test";

            while (!str.EndOfStream)
            {
                foreach (string line in str.ReadLine())
                {
                    string[] teil = line.Split(';');
                    string vornameTest = teil[0];
                    string nameTest = teil[1];
                    if (vorname.Equals(vornameTest) || name.Equals(nameTest))
                    {
                        
                        while (hallo != null)
                        {
                            hallo = str.ReadLine();
                            foreach (string line1 in hallo)
                            {
                                string[] teil1 = line1.Split(';');
                                int anzahl = int.Parse(teil1[0]);
                                double wert = double.Parse(teil1[1]);
                                string typ = teil1[2];
                                string Tname = teil1[3];
                                string isinNummer = teil1[4];

                                switch (typ)
                                {
                                   case "ETF": string basis = teil1[5];
                                    liste.Add(new WertpapierPosten(anzahl, wert, new ETF(Tname, isinNummer, basis)));
                                        break;
                                    case "Optionsschein": DateTime datum = DateTime.Parse(teil1[5]); string Bezeichner = teil1[6];
                                        liste.Add(new WertpapierPosten(anzahl, wert, new Optionsschein(Tname, isinNummer, datum, Bezeichner)));
                                        break;

                                    case "Aktie": string kuerzel = teil1[5]; double devidende = double.Parse(teil1[6]);
                                        liste.Add(new WertpapierPosten(anzahl, wert, new Aktie(Tname, isinNummer, kuerzel, devidende)));
                                        break;
                                    case "Anleihen": DateTime laufzeit = DateTime.Parse(teil1[5]); double koubonwert = double.Parse(teil1[6]);
                                        liste.Add(new WertpapierPosten(anzahl, wert, new Anleihen(Tname, isinNummer, laufzeit, koubonwert)));
                                        break;

                                }

                            }
                        }
                    }
                }
            }
            return liste;
        }


    }
}
