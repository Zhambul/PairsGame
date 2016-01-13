using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using PairsGame.Annotations;

namespace PairsGame.Bl
{
    class GameTable : IGameTable, INotifyPropertyChanged
    {
        private bool _isOpeningNewElement;

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

        private IGameElement _currentElement;
        public IGameElementFabric GameElementFabric { get; set; }
        
        private readonly int DISABLED_BUTTON_TAG = -1;
        private IGameElement CurrentElement
        {
            get { return _currentElement; }
            set
            {
                if (_isOpeningNewElement)
                {
                    PreviousElement = CurrentElement;
                }
                _isOpeningNewElement = !_isOpeningNewElement;
                _currentElement = value;
            }
        }
        private IGameElement PreviousElement { get; set; }

        private int _idSetter;

        public GameTable()
        {
            GameElements = new ObservableCollection<IGameElement>();
        }

        public void FillWithElements(int size, UniformGrid MyGrid)
        {

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    IGameElement gameElement = GameElementFabric.GetGameElement();

                    RegisterGameElement(gameElement , i , j);

                    MyGrid.Children.Add(gameElement.GameButton);
                    GameElements.Add(gameElement);
                }
            }
        }

        private void SetIdToGameElement(IGameElement element)
        {
            element.GameButton.Tag = _idSetter;
            _idSetter++;
        }

        private IGameElement FindGameElementById(int id)
        {
            if (id == DISABLED_BUTTON_TAG)
            {
                return null;
            }
            foreach (var gameElement in _gameElements)
            {
                if ((int)gameElement.GameButton.Tag == id)
                {
                    return gameElement;
                }
            }
            throw new Exception("Element is not found");
        }
        private void GameButton_Click(object sender, RoutedEventArgs e)
        {
            MakeMove( (Button) e.OriginalSource);
        }

        public void MakeMove(Button button)
        {
            if (PreviousElement != null)
            {
                ResetPair();
            }

            CurrentElement = FindGameElementById((int) button.Tag);

            if (CurrentElement != null)
            {
                CurrentElement.SetSelected();

                if (IsPairMatches())
                {
                    DisablePair();
                }
            }
            else
            {
                ResetPair();
            }
            CheckWin();
        }

        private void CheckWin()
        {
            if (GameElements.Count == 0)
            {
                MessageBox.Show("You have won");
            }
        }

        private bool IsPairMatches()
        {
            if (PreviousElement != null)
            {
                return (Equals(PreviousElement.FrontImage.Id, CurrentElement.FrontImage.Id));
            }
            return false;
        }

        private void ResetPair()
        {
            CurrentElement = null;
            PreviousElement = null;
            _isOpeningNewElement = false;

            NotifyGameElements();
        }

        private void DisablePair()
        {

            NotifyGameElements();

            RemoveGameElement(CurrentElement);
            RemoveGameElement(PreviousElement);

        }

        public void RegisterGameElement(IGameElement gameElement, int i, int j)
        {
            gameElement.GameButton.Click += GameButton_Click;
            SetIdToGameElement(gameElement);

            gameElement.GameButton.SetValue(Grid.RowProperty, i);
            gameElement.GameButton.SetValue(Grid.ColumnProperty, j);
        }

        public void RemoveGameElement(IGameElement gameElement)
        {
            gameElement.DisableElement();
            gameElement.GameButton.Tag = DISABLED_BUTTON_TAG;
            _gameElements.Remove(gameElement);
        }

        public void NotifyGameElements()
        {
            foreach (var gameElement in _gameElements)
            {
                gameElement.Update();
            }
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
