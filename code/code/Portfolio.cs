using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.ComponentModel;
using System.Data;

namespace code
{
    public class Portfolio
    {
        public List<WertpapierPosten> WertpapierListe { get; set; }
        string datei = "Personendaten.txt";
        string vorname;
        string name;

        public Portfolio(string vorname, string name)
        {
            this.vorname = vorname;
            this.name = name;
            List<WertpapierPosten> liste = new List<WertpapierPosten>();

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
                                    double dividende = double.Parse(teil1[6]);
                                    liste.Add(new WertpapierPosten(anzahl, wert, new Aktie(Tname, isinNummer, kuerzel, dividende)));
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
            WertpapierListe = liste;
        }

        

        public void Portfoliospeichern()
        {
            StreamWriter stw = new StreamWriter(datei, true);
            stw.WriteLine(vorname + ";" + name);
            foreach (WertpapierPosten item in WertpapierListe)
            {
                if(item.Wertpapier is ETF etf)
                {
                    stw.WriteLine(item.Anzahl + ";" + item.Preis + ";ETF;" + etf.Name + ";" + etf.ISIN_Nummer + ";" + etf.Basis);
                }
                if (item.Wertpapier is Aktie aktie)
                {
                    stw.WriteLine(item.Anzahl + ";" + item.Preis + ";Aktie;" + aktie.Name + ";" + etf.ISIN_Nummer + ";" + aktie.Kuerzel + ";" + aktie.Dividende);
                }
                if (item.Wertpapier is Anleihen anleihen)
                {
                    stw.WriteLine(item.Anzahl + ";" + item.Preis + ";Anleihen;" + anleihen.Name + ";" + anleihen.ISIN_Nummer + ";" + anleihen.Laufzeit + ";" + anleihen.Couponwert);
                }
                if (item.Wertpapier is Optionsschein optionsschein)
                {
                    stw.WriteLine(item.Anzahl + ";" + item.Preis + ";Optionsschein;" + optionsschein.Name + ";" + optionsschein.ISIN_Nummer + ";" + optionsschein.LaufzeitEnde + ";" + optionsschein.Bezeichner);
                }
            }
            stw.WriteLine();
            stw.Close();
        }
    }
}