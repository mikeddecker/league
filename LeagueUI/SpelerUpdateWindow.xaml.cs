using LeagueBL.DTO;
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
    /// Interaction logic for SpelerUpdateWindow.xaml
    /// </summary>
    public partial class SpelerUpdateWindow : Window {
        private SpelerManager spelerManager;
        public SpelerUpdateWindow() {
            InitializeComponent();
            spelerManager = new SpelerManager(new SpelerRepoADO(ConfigurationManager.ConnectionStrings["LeagueDBConnection"].ToString()));
        }
        private void ZoekSpelerButton_Click(object sender, RoutedEventArgs args) {
            int? spelerId = null;
            string naam = null;
            if (!string.IsNullOrWhiteSpace(ZoekSpelerNaamTextBox.Text)) { naam = ZoekSpelerNaamTextBox.Text; }
            if (!string.IsNullOrWhiteSpace(ZoekSpelerIdTextBox.Text)) { spelerId = int.Parse(ZoekSpelerIdTextBox.Text); }
            IReadOnlyList<SpelerInfo> spelers = spelerManager.SelecteerSpelers(spelerId, naam);
            if (spelers.Count == 0) {
                SpelerIdTextBox.Text = "";
                NaamTextBox.Text = "";
                LengteTextBox.Text = "";
                GewichtTextBox.Text = "";
                TeamTextBox.Text = "";
                RugnummerTextBox.Text = "";
            } else if (spelers.Count() == 1) {
                NaamTextBox.Text = spelers[0].Naam;
                SpelerIdTextBox.Text = spelers[0].Id.ToString();
                LengteTextBox.Text = spelers[0].Lengte.ToString();
                GewichtTextBox.Text = spelers[0].Gewicht.ToString();
                RugnummerTextBox.Text = spelers[0].Rugnummer.ToString();
                TeamTextBox.Text = spelers[0].TeamNaam;

            }
            if (spelers.Count() > 1) {
                SelecteerSpelerWindow selecteerSpelerWindow = new SelecteerSpelerWindow(spelers);
                if (selecteerSpelerWindow.ShowDialog() == true) {
                    NaamTextBox.Text = selecteerSpelerWindow.SelectedSpeler.Naam;
                    SpelerIdTextBox.Text = selecteerSpelerWindow.SelectedSpeler.Id.ToString();
                    LengteTextBox.Text = selecteerSpelerWindow.SelectedSpeler.Lengte.ToString();
                    GewichtTextBox.Text = selecteerSpelerWindow.SelectedSpeler.Gewicht.ToString();
                    RugnummerTextBox.Text = selecteerSpelerWindow.SelectedSpeler.Rugnummer.ToString();
                    TeamTextBox.Text = selecteerSpelerWindow.SelectedSpeler.TeamNaam;
                }
            }
        }


        private void SpelerUpdateButton_Click(object sender, RoutedEventArgs e) {
            try {
                int spelerId = int.Parse(SpelerIdTextBox.Text);
                int? lengte = null;
                if (!string.IsNullOrWhiteSpace(LengteTextBox.Text)) { lengte = int.Parse(LengteTextBox.Text); }
                int? gewicht = null;
                if (!string.IsNullOrWhiteSpace(GewichtTextBox.Text)) { gewicht = int.Parse(GewichtTextBox.Text); }
                int? rugnummer = null;
                if (!string.IsNullOrWhiteSpace(RugnummerTextBox.Text)) { rugnummer = int.Parse(RugnummerTextBox.Text); }
                SpelerInfo spelerInfo = new SpelerInfo(spelerId, NaamTextBox.Text, lengte, gewicht, rugnummer, TeamTextBox.Text);
                spelerManager.UpdateSpeler(spelerInfo);
                MessageBox.Show($"speler : {spelerInfo}", "Speler is up-to-date");
                Close();
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
