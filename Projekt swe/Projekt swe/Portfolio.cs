using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Projekt_swe
{
    public class Portfolio
    {
        public List<WertpapierPosten> WertpapierListe { get; set; }
        private string datei = "Personendaten.txt";
        private string vorname;
        private string nachname;

        public Portfolio(string vorname, string nachname)
        {
            this.vorname = vorname;
            this.nachname = nachname;
            WertpapierListe = new List<WertpapierPosten>();
            LadePortfolio();
        }

        // Portfolio aus der Datei laden
        public void LadePortfolio()
        {
            if (!File.Exists(datei))
            {
                Console.WriteLine("Datei nicht gefunden.");
                return; // Keine Datei vorhanden, also leeres Portfolio
            }

            using (StreamReader reader = new StreamReader(datei))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var daten = line.Split(';');
                    if (daten[0] == vorname && daten[1] == nachname)
                    {
                        while ((line = reader.ReadLine()) != null && !string.IsNullOrWhiteSpace(line))
                        {
                            var teile = line.Split(';');
                            int anzahl = int.Parse(teile[0]);
                            double preis = double.Parse(teile[1]);
                            string typ = teile[2];
                            string name = teile[3];
                            string isin = teile[4];

                            // Erstelle das entsprechende Wertpapier
                            Wertpapier wertpapier = null;
                            switch (typ)
                            {
                                case "ETF":
                                    wertpapier = new ETF(name, isin, teile[5]);
                                    break;
                                case "Aktie":
                                    wertpapier = new Aktie(name, isin, teile[5], double.Parse(teile[6]));
                                    break;
                                case "Anleihen":
                                    wertpapier = new Anleihe(name, isin, DateTime.Parse(teile[5]), double.Parse(teile[6]));
                                    break;
                            }

                            if (wertpapier != null)
                            {
                                WertpapierListe.Add(new WertpapierPosten(anzahl, preis, wertpapier));
                            }
                        }
                    }
                }
            }
        }

        // Portfolio in die Datei speichern
        public void Portfoliospeichern()
        {
            var alleZeilen = File.Exists(datei) ? File.ReadAllLines(datei).ToList() : new List<string>();
            alleZeilen.RemoveAll(l => l.StartsWith($"{vorname};{nachname}")); // Alten Eintrag entfernen

            using (StreamWriter writer = new StreamWriter(datei, false))
            {
                // Überschreibe alle alten Einträge (außer den gelöschten)
                foreach (var zeile in alleZeilen)
                {
                    writer.WriteLine(zeile);
                }

                // Neuen Benutzer und seine Wertpapiere speichern
                writer.WriteLine($"{vorname};{nachname}");
                foreach (var posten in WertpapierListe)
                {
                    if (posten.Wertpapier is ETF etf)
                    {
                        writer.WriteLine($"{posten.Anzahl};{posten.Preis};ETF;{etf.Name};{etf.ISIN};{etf.Basis}");
                    }
                    else if (posten.Wertpapier is Aktie aktie)
                    {
                        writer.WriteLine($"{posten.Anzahl};{posten.Preis};Aktie;{aktie.Name};{aktie.ISIN};{aktie.Kuerzel};{aktie.Dividende}");
                    }
                    else if (posten.Wertpapier is Anleihe anleihe)
                    {
                        writer.WriteLine($"{posten.Anzahl};{posten.Preis};Anleihen;{anleihe.Name};{anleihe.ISIN};{anleihe.Laufzeit};{anleihe.Couponwert}");
                    }
                }
            }
        }

        public void Kaufen(WertpapierPosten neuerPosten)
        {
            // Prüfen, ob das Wertpapier bereits im Portfolio vorhanden ist
            var vorhandenerPosten = WertpapierListe.FirstOrDefault(p => p.Wertpapier.ISIN == neuerPosten.Wertpapier.ISIN);

            if (vorhandenerPosten != null)
            {
                // Wenn das Wertpapier bereits im Portfolio ist, die Anzahl erhöhen
                vorhandenerPosten.Anzahl += neuerPosten.Anzahl;
                Console.WriteLine($"Bestand erhöht: {neuerPosten.Anzahl}x {neuerPosten.Wertpapier.Name} hinzugefügt. Neue Anzahl: {vorhandenerPosten.Anzahl}");
            }
            else
            {
                // Wenn das Wertpapier nicht im Portfolio ist, es neu hinzufügen
                WertpapierListe.Add(neuerPosten);
                Console.WriteLine($"Gekauft: {neuerPosten.Anzahl}x {neuerPosten.Wertpapier.Name} zu {neuerPosten.Preis} pro Stück");
            }
        }


        public void Verkaufen(Wertpapier wertpapier, int anzahl)
        {
            var posten = WertpapierListe.FirstOrDefault(p => p.Wertpapier == wertpapier);
            if (posten != null && posten.Anzahl >= anzahl)
            {
                posten.Anzahl -= anzahl;
                if (posten.Anzahl == 0) WertpapierListe.Remove(posten);
                Console.WriteLine($"Verkauft: {anzahl}x {wertpapier.Name}");
            }
            else
            {
                Console.WriteLine("Verkauf nicht möglich.");
            }
        }
    }
}
