using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;

namespace Projekt_swe
{
    public partial class MainWindow : Window
    {
        private Bank bank;
        private Kunde kunde;
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
        private string currentUser;

        public MainWindow(string user)
        {
            InitializeComponent();
            currentUser = user;

            // Benutzer-Vorname und Nachname extrahieren
            string[] nameParts = currentUser.Split(' ');
            string vorname = nameParts[0];
            string nachname = nameParts.Length > 1 ? nameParts[1] : "";

            // Portfolio und Bank initialisieren
            InitializeData(vorname, nachname);
            LoadAvailableSecurities();
            LoadPortfolio();

            MessageBox.Show($"Willkommen, {currentUser}!", "Login Erfolgreich");
        }

        private void InitializeData(string vorname, string nachname)
        {
            // Liste der Wertpapiere initialisieren
            var wertpapierListe = new List<Wertpapier>
            {
                new Aktie("Apple", "US0378331005", "AAPL", 0.1),
                new ETF("DAX ETF", "DE000ETFL", "DAX"),
                new Anleihe("PCC Anleihe", "DE000A351K90", new DateTime(2028, 7, 1), 5.0),
                new Optionsschein("Option AAPL", "US0378331004", new DateTime(2024, 10, 15), "PUT")
            };

            // Bank und Kursverläufe initialisieren
            bank = new Bank(wertpapierListe);
            GenerateKursverlaeufe();

            // Kunde und Portfolio laden
            kunde = new Kunde(nachname, vorname, new Portfolio(vorname, nachname), 10000);

            // Chart initialisieren
            SeriesCollection = new SeriesCollection();
            Labels = new string[0];
            Formatter = value => value.ToString("N");
            chartKursverlauf.Series = SeriesCollection;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Login-Fenster öffnen und aktuelles Fenster schließen
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close(); // Schließt das aktuelle Fenster (MainWindow)
        }

        // Methode für die Auswahländerung in der Liste der verfügbaren Wertpapiere
        private void lstAvailableSecurities_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lstAvailableSecurities.SelectedItem is Wertpapier selectedWertpapier)
            {
                // Lade den Kursverlauf des ausgewählten Wertpapiers
                var values = selectedWertpapier.KursListe.Select(k => k.Wert).ToArray();
                Labels = selectedWertpapier.KursListe.Select(k => k.Datum.Day.ToString()).ToArray();

                SeriesCollection.Clear();
                SeriesCollection.Add(new LiveCharts.Wpf.LineSeries
                {
                    Title = selectedWertpapier.Name,
                    Values = new LiveCharts.ChartValues<double>(values)
                });

                chartKursverlauf.AxisX[0].Labels = Labels;
                DataContext = this;
            }
        }

        // Kursverläufe der Wertpapiere generieren (zufällige Daten)
        private void GenerateKursverlaeufe()
        {
            var random = new Random();
            foreach (var wertpapier in bank.WertpapierListe)
            {
                double lastValue = random.Next(50, 81);
                for (int i = 1; i <= 14; i++)
                {
                    lastValue = Math.Max(0, lastValue + random.NextDouble() * 10 - 5);
                    wertpapier.KursListe.Add(new Kurs(DateTime.Now.AddDays(i), lastValue));
                }
            }
        }

        // Verfügbare Wertpapiere in der ListBox anzeigen (Name und ISIN)
        private void LoadAvailableSecurities()
        {
            lstAvailableSecurities.ItemsSource = bank.WertpapierListe;
        }

        // Portfolio des Benutzers in der ListBox anzeigen (Name, ISIN und Anzahl)
        private void LoadPortfolio()
        {
            lstPortfolio.ItemsSource = null;
            lstPortfolio.ItemsSource = kunde.Portfolio.WertpapierListe.Select(p => new PortfolioItem
            {
                WertpapierName = p.Wertpapier.Name,
                ISIN = p.Wertpapier.ISIN,
                Anzahl = p.Anzahl
            }).ToList();
        }


        // Event-Handler für den Kauf-Button
        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            if (lstAvailableSecurities.SelectedItem is Wertpapier selectedWertpapier && int.TryParse(txtQuantityToBuy.Text, out int quantity))
            {
                if (quantity > 0)
                {
                    // Kauf durchführen
                    bank.Kaufen(kunde, selectedWertpapier, quantity);
                    kunde.Portfolio.Kaufen(new WertpapierPosten(quantity, selectedWertpapier.AktuellerKurs, selectedWertpapier));
                    kunde.Portfolio.Portfoliospeichern();

                    // Portfolio nach dem Kauf neu laden
                    LoadPortfolio();
                    MessageBox.Show($"{quantity}x {selectedWertpapier.Name} gekauft!");
                }
                else
                {
                    MessageBox.Show("Die Anzahl muss größer als 0 sein.");
                }
            }
            else
            {
                MessageBox.Show("Bitte eine gültige Anzahl eingeben und ein Wertpapier auswählen.");
            }
        }

        private void SellButton_Click(object sender, RoutedEventArgs e)
        {
            if (lstPortfolio.SelectedItem is PortfolioItem selectedPosten && int.TryParse(txtQuantityToSell.Text, out int quantity))
            {
                // Das WertpapierPosten-Objekt basierend auf dem ausgewählten PortfolioItem abrufen
                var wertpapierPosten = kunde.Portfolio.WertpapierListe.FirstOrDefault(p => p.Wertpapier.Name == selectedPosten.WertpapierName);

                if (wertpapierPosten != null && quantity > 0 && wertpapierPosten.Anzahl >= quantity)
                {
                    // Verkauf durchführen
                    bank.Verkaufen(kunde, wertpapierPosten.Wertpapier, quantity);
                    kunde.Portfolio.Verkaufen(wertpapierPosten.Wertpapier, quantity);
                    kunde.Portfolio.Portfoliospeichern();

                    // Portfolio nach dem Verkauf neu laden
                    LoadPortfolio();
                    MessageBox.Show($"{quantity}x {wertpapierPosten.Wertpapier.Name} verkauft!");
                }
                else
                {
                    MessageBox.Show("Bitte eine gültige Anzahl auswählen oder sicherstellen, dass genug Aktien vorhanden sind.");
                }
            }
            else
            {
                MessageBox.Show("Bitte eine gültige Anzahl und ein Wertpapier aus Ihrem Portfolio auswählen.");
            }
        }

    }
}
