using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using PairsGame.Annotations;
using PairsGame.Bl;

namespace PairsGame.Ui.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private IGameTable _gameTable;

        private ObservableCollection<IGameElement> _gameElements;
        public ObservableCollection<IGameElement> GameElements
        {
            get { return _gameElements; }

            set
            {
                _gameElements = value;
                OnPropertyChanged();
            }
        }

        private int SIZE = 4;
        public MainWindow()
        {
            DataContext = this;
            GameElements = new ObservableCollection<IGameElement>();
            InitializeComponent();

            FillGridWithElements();
        }

        private void FillGridWithElements()
        {
            _gameTable = new GameTable()
            {
                ImageFabric = new ImageFabric(SIZE),
                GameElementFabric = new GameElementFabric()
            };

            _gameTable.FillWithElements(SIZE, MyGrid);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
