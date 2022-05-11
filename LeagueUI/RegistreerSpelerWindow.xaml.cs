using LeagueBL.Managers;
using LeagueDL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LeagueUI {
    /// <summary>
    /// Interaction logic for RegistreerSpeler.xaml
    /// </summary>
    public partial class RegistreerSpelerWindow : Window {
        private SpelerManager spelerManager;
        public RegistreerSpelerWindow() {
            InitializeComponent();
            spelerManager = new SpelerManager(new SpelerRepoADO(ConfigurationManager.ConnectionStrings["LeagueDBConnection"].ToString()));
        }

        private void RegistreerSpelerButton_Click(object sender, RoutedEventArgs e) {
            try {
                string naam = null;
                int? lengte = null;
                int? gewicht = null;
                int? rugnummer = null;

                if (string.IsNullOrWhiteSpace(NaamTextBox.Text)) {
                    MessageBox.Show("Naam is leeg");
                } else {
                    naam = NaamTextBox.Text;
                    if (!string.IsNullOrWhiteSpace(LengteTextBox.Text)) { lengte = int.Parse(LengteTextBox.Text); }
                    if (!string.IsNullOrWhiteSpace(GewichtTextBox.Text)) { gewicht = int.Parse(GewichtTextBox.Text); }
                    if (!string.IsNullOrWhiteSpace(RugnummerTextBox.Text)) { rugnummer = int.Parse(RugnummerTextBox.Text); }
                    spelerManager.RegistreerSpeler(naam, lengte, gewicht);
                    MessageBox.Show($"{naam} is toegevoegd", "Registreer Speler");
                    Close();
                }

            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "Registreer Speler");
            }

        }
    }
}
