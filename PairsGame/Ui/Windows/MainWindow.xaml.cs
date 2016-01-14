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
    public partial class MainWindow : MetroWindow
    {
        private readonly int _size;
        // объект игры
        private IGameTable _gameTable;

        // конструктор принимает сложность игры
        public MainWindow(int size)
        {
            _size = size;
            InitializeComponent();

            FillGridWithElements();
        }

        private void FillGridWithElements()
        {
            // инициализируем игру
            _gameTable = new GameTable(new GameElementStackFabric(new GameElementFabric(new ImageFabric(_size * _size))));
            
            // заполняем элементами доску
            _gameTable.FillWithElements(_size, MyGrid);
        }
    }
}
