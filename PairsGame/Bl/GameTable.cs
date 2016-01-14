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
    // класс "доски", логика игры
    class GameTable : IGameTable
    {
        // флаг, означающий, открывается ли следующий элемент
        private bool _isOpeningNewElement;

        // коллекция стеков элементов игры
        public List<GameElementsStack> GameElementStacks; 
        
        private IGameElement _currentElement;

        private readonly IGameElementsStackFabric _gameElementsStackFabric;

        // id для уже сыгранного элемента
        private readonly int DISABLED_BUTTON_TAG = -1;

        private Grid _grid;

        // элемент который выбран, в сетторе ставится нынешний элемент как предыдущий, а нынешний как новый
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

        // предыдущий элемент
        private IGameElement PreviousElement { get; set; }

        // переменная, значение которой сеттит айдишники и инкрементируется
        private int _idSetter;

        // конструктор
        public GameTable(IGameElementsStackFabric gameElementsStackFabric)
        {
            _gameElementsStackFabric = gameElementsStackFabric;
            GameElementStacks = new List<GameElementsStack>();
        }

        // заполнение элементами доски
        public void FillWithElements(int size, Grid myGrid)
        {
            // передается ссылка на вьюшку, куда заливается
            _grid = myGrid;

            // цикл для заполнения
            for (var i = 0; i < size; i++)
            {   
                // добавление строк и столбов в доску
                _grid.RowDefinitions.Add(new RowDefinition());
                _grid.ColumnDefinitions.Add(new ColumnDefinition());

                for (var j = 0; j < size; j++)
                {
                    // создание стака элементов
                    var gameElementsStack = _gameElementsStackFabric.Get();

                    var gameElement = gameElementsStack.Elements.Peek();

                    RegisterGameElement(gameElement, i, j);
                    
                    GameElementStacks.Add(gameElementsStack);

                }

            }
        }
        // "регистрация" элемента игры в доску
        public void RegisterGameElement(IGameElement gameElement, int i, int j)
        {
            gameElement.GameButton.Click += GameButton_Click;
            SetIdToGameElement(gameElement);

            gameElement.GameButton.SetValue(Grid.RowProperty, i);
            gameElement.GameButton.SetValue(Grid.ColumnProperty, j);

            _grid.Children.Add(gameElement.GameButton);
        }

        // сеттер id на элемент
        private void SetIdToGameElement(IGameElement element)
        {
            element.GameButton.Tag = _idSetter;
            _idSetter++;
        }

        // возвращает элемент по его id
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
        // метод, вызывающийся при клики на каждый элемент
        private void GameButton_Click(object sender, RoutedEventArgs e)
        {
            MakeMove( (Button) e.OriginalSource);
        }
        // главный алгоритм хода
        public void MakeMove(Button button)
        {
            if (PreviousElement != null)
            {
                ResetPair();
            }

            // находим нынешний элемент
            CurrentElement = FindGameElementById((int) button.Tag);

            if (CurrentElement != null)
            {
                // анимация выборки
                CurrentElement.SetSelected();

                // если сходится с предыдущим
                if (IsPairMatches())
                {
                    // то удаляется оба из стека
                    DisablePair();
                }
            }
            else
            {
                ResetPair();
            }
            // проверка победы
            CheckWin();
        }

        // проверка победы
        private void CheckWin()
        {
            if (GameElementStacks.Count == 0)
            {
                MessageBox.Show("You have won");
            }
        }

        // метод проверяющий сходятся ли пред и нынешний элементы
        private bool IsPairMatches()
        {
            if (PreviousElement != null)
            {
                return (Equals(PreviousElement.FrontImage.Id, CurrentElement.FrontImage.Id) &&
                        !Equals(PreviousElement.GameButton.Tag, CurrentElement.GameButton.Tag));
            }
            return false;
        }

        // обнуление указателей на пред и нын элементы
        private void ResetPair()
        {
            CurrentElement = null;
            PreviousElement = null;
            _isOpeningNewElement = false;

            NotifyGameElements();
        }
        //удаление элементов
        private void DisablePair()
        {
            NotifyGameElements();

            RemoveGameElement(CurrentElement);
            RemoveGameElement(PreviousElement);
        }
       
        // удаление элемента игры из стека
        public void RemoveGameElement(IGameElement gameElement)
        {
            gameElement.DisableElement();
            gameElement.GameButton.Tag = DISABLED_BUTTON_TAG;

            GameElementsStack currentGameElementStack = null;
            
            // ищем стек элемента на котором находится gameElement
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

            // если стек не пуст
            if (currentGameElementStack.Elements.Count == 2)
            {
                // то удаляем верхний элемент, показываем элемент, который идет дальше по стеку
                currentGameElementStack.Elements.Pop();
                var nextGameElement = currentGameElementStack.Elements.Peek();
                
                var row = (int) gameElement.GameButton.GetValue(Grid.RowProperty);
                var column = (int) gameElement.GameButton.GetValue(Grid.ColumnProperty);

                RegisterGameElement(nextGameElement, row, column);
                _grid.Children.Remove(gameElement.GameButton);
            }
            // иначе удаляем стек    
            else
            {
                GameElementStacks.Remove(currentGameElementStack);
            }
        }

        // метод для оповещения всех элементов для снятия анимации выборки
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
