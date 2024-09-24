using System;
using System.Collections.Generic;
using System.Windows;
using System.Net.Http;
using System.Globalization;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Projekt_swe
{
    public partial class MainWindow : Window
    {
        private Dictionary<string, Wertpapier> wertpapierListe = new Dictionary<string, Wertpapier>();
        private Dictionary<string, Portfolio> portfolioListe = new Dictionary<string, Portfolio>();
        private const string ApiSchlüssel = "3GXOPWZ0Z9OBUN5U";
        private const string BasisApiUrl = "https://www.alphavantage.co/query";

        public MainWindow()
        {
            InitializeComponent();
            AktualisiereWertpapierListe();
        }

        // Aktualisieren des Wertpapier-Tab-Elements
        private void AktualisiereWertpapierListe()
        {
            WertpapierListe.Items.Clear();
            foreach (var wertpapier in wertpapierListe.Values)
            {
                WertpapierListe.Items.Add($"{wertpapier.Name} ({wertpapier.ISIN_Nummer})");
            }
        }

        // Aktualisieren des Portfolio-Tab-Elements
        private void AktualisierePortfolio_Click(object sender, RoutedEventArgs e)
        {
            PortfolioListe.Items.Clear();
            foreach (var portfolio in portfolioListe.Values)
            {
                PortfolioListe.Items.Add($"Portfolio: {portfolio.Name}, Totalwert: {portfolio.BerechnePortfolioWert():C}");
            }
        }

        // Kaufen eines Wertpapiers
        private void KaufeWertpapier_Click(object sender, RoutedEventArgs e)
        {
            if (WertpapierListe.SelectedItem == null)
            {
                MessageBox.Show("Bitte wählen Sie ein Wertpapier aus der Liste.");
                return;
            }

            string ausgewähltesWertpapier = WertpapierListe.SelectedItem.ToString().Split(' ')[0];
            if (wertpapierListe.ContainsKey(ausgewähltesWertpapier))
            {
                var wertpapier = wertpapierListe[ausgewähltesWertpapier];

                if (!portfolioListe.ContainsKey("MeinPortfolio"))
                {
                    portfolioListe["MeinPortfolio"] = new Portfolio("MeinPortfolio");
                }

                portfolioListe["MeinPortfolio"].FügeWertpapierHinzu(wertpapier, 1); // 1 Stück hinzufügen als Beispiel
                MessageBox.Show($"Sie haben 1 Stück von {wertpapier.Name} gekauft.");
            }
        }

        // Verkaufen eines Wertpapiers
        private void VerkaufeWertpapier_Click(object sender, RoutedEventArgs e)
        {
            if (WertpapierListe.SelectedItem == null)
            {
                MessageBox.Show("Bitte wählen Sie ein Wertpapier aus der Liste.");
                return;
            }

            string ausgewähltesWertpapier = WertpapierListe.SelectedItem.ToString().Split(' ')[0];
            if (wertpapierListe.ContainsKey(ausgewähltesWertpapier) && portfolioListe.ContainsKey("MeinPortfolio"))
            {
                var wertpapier = wertpapierListe[ausgewähltesWertpapier];
                portfolioListe["MeinPortfolio"].EntferneWertpapier(wertpapier, 1); // 1 Stück entfernen als Beispiel
                MessageBox.Show($"Sie haben 1 Stück von {wertpapier.Name} verkauft.");
            }
        }

        // Anzeige des Kursverlaufs eines Wertpapiers
        private void ZeigeKursverlauf_Click(object sender, RoutedEventArgs e)
        {
            if (WertpapierAuswahl.SelectedItem == null)
            {
                MessageBox.Show("Bitte wählen Sie ein Wertpapier aus.");
                return;
            }

            string ausgewähltesWertpapier = WertpapierAuswahl.SelectedItem.ToString();
            if (wertpapierListe.ContainsKey(ausgewähltesWertpapier))
            {
                KursverlaufListe.Items.Clear();
                foreach (var kurs in wertpapierListe[ausgewähltesWertpapier].kursListe)
                {
                    KursverlaufListe.Items.Add($"{kurs.Datum.ToShortDateString()}: {kurs.Wert:C}");
                }
            }
        }

        // Beispielmethode
        private async void LadeAktienDaten(string symbol)
        {
            try
            {
                HttpClient client = new HttpClient();
                string apiUrl = $"{BasisApiUrl}?function=TIME_SERIES_DAILY&symbol={symbol}&apikey={ApiSchlüssel}";

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();

                // Deserialisiere die JSON-Antwort in ein Dictionary
                var daten = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);

                // Extrahiere die "Time Series (Daily)" Daten
                var zeitreihe = daten["Time Series (Daily)"] as Dictionary<string, Dictionary<string, string>>;

                if (zeitreihe != null)
                {
                    List<Kurs> kursListe = new List<Kurs>();

                    foreach (var tag in zeitreihe)
                    {
                        string datum = tag.Key;

                        // Hole den Schlusskurs ("4. close")
                        if (tag.Value.TryGetValue("4. close", out string closePreisStr))
                        {
                            double preis = Convert.ToDouble(closePreisStr, CultureInfo.InvariantCulture);

                            // Füge den Kurs zur Liste hinzu
                            kursListe.Add(new Kurs(DateTime.Parse(datum), preis));
                        }
                    }

                    // Prüfe, ob die Aktie schon in der Liste ist, wenn nicht, füge sie hinzu
                    if (!wertpapierListe.ContainsKey(symbol))
                    {
                        Aktie aktie = new Aktie("Aktie", symbol, symbol, 0);
                        aktie.kursListe = kursListe;
                        wertpapierListe.Add(symbol, aktie);
                    }

                    // Aktualisiere die Wertpapier-Liste
                    AktualisiereWertpapierListe();
                }
                else
                {
                    MessageBox.Show("Keine Daten gefunden.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Abrufen der Daten: {ex.Message}");
            }
        }

    }
}
