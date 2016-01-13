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

        private GameElement[,] _gameElementsArray;
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

        public void FillWithElements(int size, UniformGrid myGrid)
        {
            _gameElementsArray = new GameElement[size+2,size+2];
            int count = 0;
            int restcount = 0;
            int allcount = 0;
            for (int row = 0; row < _gameElementsArray.GetLength(0); row++)
            {
                for (int col = 0; col < _gameElementsArray.GetLength(1); col++)
                {
                    try
                    {
                        GameElement result;

                        if (row > 0 && col > 0 && col < size + 1 && row < size + 1 )
                        {
                            var gameElement = GameElementFabric.GetGameElement();

                            RegisterGameElement(gameElement, row, col);
                            myGrid.Children.Add(gameElement.GameButton);
                            GameElements.Add(gameElement);
                            result = (GameElement) gameElement;
                            result.Empty = false;
                            count ++;
                        }
                        else
                        {
                            result = new GameElement {Empty = true};
                            restcount++;
                        }
                        allcount++;
                        result.Row = row;
                        result.Col = col;

                        _gameElementsArray[row, col] = result;
                    }
                    catch (Exception e)
                    {
                        
                    }
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

                if (PreviousElement != null)
                {
                    new RouteHelper().MakeRoute(_gameElementsArray, CurrentElement,PreviousElement);

                    for (int row = 0; row < _gameElementsArray.GetLength(0); row++)
                    {
                        for (int col = 0; col < _gameElementsArray.GetLength(1); col++)
                        {
                            _gameElementsArray[row, col].Checked = false;
                        }
                    }
                }
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
                return (Equals(PreviousElement.FrontImage.Id, CurrentElement.FrontImage.Id) &&
                       !Equals(PreviousElement.GameButton.Tag, CurrentElement.GameButton.Tag));
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
            RemoveGameElement(CurrentElement);
            RemoveGameElement(PreviousElement);

            NotifyGameElements();
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
            gameElement.Empty = true;
            gameElement.GameButton.Tag = DISABLED_BUTTON_TAG;
//            _gameElements.Remove(gameElement);    
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
