using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LeagueUI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void RegistreerSpelerButton_Click(object sender, RoutedEventArgs e) {
            RegistreerSpelerWindow registreerSpelerWindow = new RegistreerSpelerWindow();
            registreerSpelerWindow.ShowDialog();
        }

        private void UpdateSpelerButton_Click(object sender, RoutedEventArgs e) {
            SpelerUpdateWindow spelerUpdateWindow = new SpelerUpdateWindow();
            spelerUpdateWindow.ShowDialog();
        }

        private void RegistreerTeamButton_Click(object sender, RoutedEventArgs e) {

        }

        private void UpdateTeamButton_Click(object sender, RoutedEventArgs e) {

        }

        private void RegistreerTransferButton_Click(object sender, RoutedEventArgs e) {
            RegistreerTransferWindow registreerTransferWindow = new RegistreerTransferWindow();
            registreerTransferWindow.ShowDialog();
        }
    }
}
