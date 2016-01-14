using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PairsGame.Annotations;

namespace PairsGame.Bl
{
    // класс элемента игры
    public class GameElement : IGameElement
    {
        // фотка 
        public ButtonBackground FrontImage { get; set; }

        // анимация выделения элемента
        public void SetSelected()
        {
            GameButton.BorderThickness = new Thickness(3);
        }
        public Button GameButton { get; set; }

        // убирать элемент при нахождении пары
        public void DisableElement()
        {
            GameButton.Background = Brushes.White;
        }

        // снятие анимации выделения эемента
        public void Update()
        {
            GameButton.BorderThickness = new Thickness(1);
        }
    }
}
