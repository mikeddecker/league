using LeagueBL.Domein;
using LeagueBL.DTO;
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
using System.Windows.Shapes;

namespace LeagueUI {
    /// <summary>
    /// Interaction logic for SelecteerSpelerWindow.xaml
    /// </summary>
    public partial class SelecteerSpelerWindow : Window {
        private IReadOnlyList<SpelerInfo> spelers;
        public SpelerInfo SelectedSpeler = null;
        public SelecteerSpelerWindow(IReadOnlyList<SpelerInfo> spelers) {
            InitializeComponent();
            this.spelers = spelers;
            SpelerListBox.ItemsSource = spelers;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e) {
            DialogResult = true;
            Close();

        }

        private void SpelerListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            OkButton.IsEnabled = true;
            SelectedSpeler = (SpelerInfo)SpelerListBox.SelectedItem;
        }
    }
}
