using System.Windows;
using System.Windows.Controls;

namespace Projekt_swe
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (cmbUsers.SelectedItem != null)
            {
                string selectedUser = (cmbUsers.SelectedItem as ComboBoxItem)?.Content.ToString();

                MainWindow mainWindow = new MainWindow(selectedUser);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Bitte wählen Sie einen Benutzer aus.");
            }
        }
    }
}
