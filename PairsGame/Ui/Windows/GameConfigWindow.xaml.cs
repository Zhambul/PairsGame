using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MahApps.Metro.Controls;

namespace PairsGame.Ui.Windows
{
    /// <summary>
    /// Interaction logic for GameConfigWindow.xaml
    /// </summary>
    public partial class GameConfigWindow : MetroWindow
    {

        public ObservableCollection<String> Sizes { get; set; }

        private int _tableSize = 2;
        private int _mode = 0;
        public GameConfigWindow()
        {
            InitializeComponent();
            Sizes = new ObservableCollection<String> {"2", "4", "6", "8" };
            DataContext = this;
        }

        private void DropDownButton_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = ((SplitButton)sender).SelectedIndex;
            _tableSize = Convert.ToInt32(Sizes[selectedIndex]);
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow(_tableSize).Show();
            Close();
        }

        private void GameModeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _mode = ((SplitButton)sender).SelectedIndex;
        }
    }
}
