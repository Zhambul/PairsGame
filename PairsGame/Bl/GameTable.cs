using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using PairsGame.Annotations;

namespace PairsGame.Bl
{
    class GameTable : IGameTable
    {
        private bool _isOpeningNewElement;

        public List<GameElementsStack> GameElementStacks; 
        
        private IGameElement _currentElement;
        private readonly IGameElementsStackFabric _gameElementsStackFabric;

        private readonly int DISABLED_BUTTON_TAG = -1;

        private Grid _grid;
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

        public GameTable(IGameElementsStackFabric gameElementsStackFabric)
        {
            _gameElementsStackFabric = gameElementsStackFabric;
            GameElementStacks = new List<GameElementsStack>();
        }

        public void FillWithElements(int size, Grid myGrid)
        {
            _grid = myGrid;

            for (var i = 0; i < size; i++)
            {   
                _grid.RowDefinitions.Add(new RowDefinition());
                _grid.ColumnDefinitions.Add(new ColumnDefinition());

                for (var j = 0; j < size; j++)
                {
                    
                    var gameElementsStack = _gameElementsStackFabric.Get();

                    var gameElement = gameElementsStack.Elements.Peek();

                    RegisterGameElement(gameElement, i, j);
                    
                    GameElementStacks.Add(gameElementsStack);

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
            foreach (var gameElementStack in GameElementStacks)
            {
                foreach (var gameElement in gameElementStack.Elements.ToList())
                {
                    if ((int?) gameElement.GameButton.Tag == id)
                    {
                        return gameElement;
                    }
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
            if (GameElementStacks.Count == 0)
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

            _grid.Children.Add(gameElement.GameButton);
        }

        public void RemoveGameElement(IGameElement gameElement)
        {
            gameElement.DisableElement();
            gameElement.GameButton.Tag = DISABLED_BUTTON_TAG;

            GameElementsStack currentGameElementStack = null;

            foreach (var gameElementsStack in GameElementStacks)
            {
                foreach (var gE in gameElementsStack.Elements.ToList())
                {
                    if (gE == gameElement)
                    {
                        currentGameElementStack = gameElementsStack;
                    }
                }
            }
            if (currentGameElementStack == null)
            {
                throw new NullReferenceException();
            }

            if (currentGameElementStack.Elements.Count == 2)
            {
                currentGameElementStack.Elements.Pop();
                var nextGameElement = currentGameElementStack.Elements.Peek();
                
                var row = (int) gameElement.GameButton.GetValue(Grid.RowProperty);
                var column = (int) gameElement.GameButton.GetValue(Grid.ColumnProperty);


                RegisterGameElement(nextGameElement, row, column);
                _grid.Children.Remove(gameElement.GameButton);
            }
            else
            {
                GameElementStacks.Remove(currentGameElementStack);
            }
        }

        public void NotifyGameElements()
        {
            foreach (var gameElementsStack in GameElementStacks)
            {
                foreach (var gameElement in gameElementsStack.Elements.ToList())
                {
                    gameElement.Update();
                }
            }
        }
    }
}
