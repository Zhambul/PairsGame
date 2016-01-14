using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using PairsGame.Annotations;
using PairsGame.Bl;

namespace PairsGame.Ui.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow, INotifyPropertyChanged
    {
        private readonly int _size;
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
        public MainWindow(int size)
        {
            _size = size;
            DataContext = this;
            GameElements = new ObservableCollection<IGameElement>();
            InitializeComponent();

            FillGridWithElements();
        }

        private void FillGridWithElements()
        {
            _gameTable = new GameTable(new GameElementStackFabric(new GameElementFabric(new ImageFabric())));

            _gameTable.FillWithElements(_size, MyGrid);
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
