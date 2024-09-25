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
            InitializeData();
            LoadAvailableSecurities();
            LoadPortfolio();
            MessageBox.Show($"Willkommen, {currentUser}!", "Login Erfolgreich");
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Zurück zum Login-Fenster
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void InitializeData()
        {
            // Beispiel-Wertpapiere
            var wertpapierListe = new List<Wertpapier>
            {
                new Aktie("Apple", "US0378331005", "AAPL", 0.1),
                new ETF("DAX ETF", "DE000ETFL", "DAX"),
                new Anleihe("PCC Anleihe", "DE000A351K90", new DateTime(2028, 7, 1), 5.0),
                new Optionsschein("Option AAPL", "US0378331004", new DateTime(2024, 10, 15), "PUT")
            };

            // Bank erstellen
            bank = new Bank(wertpapierListe);

            // Kursverläufe für die Wertpapiere nur einmal generieren
            GenerateKursverlaeufe();

            // Portfolio und Kunde erstellen
            Portfolio portfolio = new Portfolio(new List<WertpapierPosten>());
            kunde = new Kunde("Müller", "Max", portfolio, 10000);

            // Initialisiere den Chart
            SeriesCollection = new SeriesCollection();
            Labels = new string[0];
            Formatter = value => value.ToString("N");
            chartKursverlauf.Series = SeriesCollection;
        }

        private void GenerateKursverlaeufe()
        {
            var random = new Random();
            foreach (var wertpapier in bank.WertpapierListe)
            {
                double lastValue = random.Next(50, 81);

                // Zufälliger Verlauf über 14 Tage
                for (int i = 1; i <= 14; i++)
                {
                    lastValue = Math.Max(0, lastValue + random.NextDouble() * 10 - 5);
                    wertpapier.KursListe.Add(new Kurs(DateTime.Now.AddDays(i), lastValue));
                }
            }
        }

        private void LoadAvailableSecurities()
        {
            lstAvailableSecurities.ItemsSource = bank.WertpapierListe;
        }

        private void LoadPortfolio()
        {
            lstPortfolio.ItemsSource = null;
            lstPortfolio.ItemsSource = kunde.Portfolio.WertpapierListe;
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            if (lstAvailableSecurities.SelectedItem is Wertpapier selectedWertpapier && int.TryParse(txtQuantityToBuy.Text, out int quantity))
            {
                bank.Kaufen(kunde, selectedWertpapier, quantity);
                LoadPortfolio();
                MessageBox.Show($"{quantity}x {selectedWertpapier.Name} gekauft!");
            }
            else
            {
                MessageBox.Show("Bitte eine gültige Anzahl eingeben und ein Wertpapier auswählen.");
            }
        }

        private void SellButton_Click(object sender, RoutedEventArgs e)
        {
            if (lstPortfolio.SelectedItem is WertpapierPosten selectedPosten && int.TryParse(txtQuantityToSell.Text, out int quantity))
            {
                bank.Verkaufen(kunde, selectedPosten.Wertpapier, quantity);
                LoadPortfolio();
                MessageBox.Show($"{quantity}x {selectedPosten.Wertpapier.Name} verkauft!");
            }
            else
            {
                MessageBox.Show("Bitte eine gültige Anzahl eingeben und ein Wertpapier aus Ihrem Portfolio auswählen.");
            }
        }

        private void lstAvailableSecurities_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lstAvailableSecurities.SelectedItem is Wertpapier selectedWertpapier)
            {
                // Lade den Kursverlauf des ausgewählten Wertpapiers
                var values = selectedWertpapier.KursListe.Select(k => k.Wert).ToArray();
                Labels = selectedWertpapier.KursListe.Select(k => k.Datum.Day.ToString()).ToArray();

                SeriesCollection.Clear();
                SeriesCollection.Add(new LineSeries
                {
                    Title = selectedWertpapier.Name,
                    Values = new LiveCharts.ChartValues<double>(values)
                });

                chartKursverlauf.AxisX[0].Labels = Labels;
                DataContext = this;
            }
        }
    }
}
