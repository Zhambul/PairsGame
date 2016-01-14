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
        public ButtonBackground FrontImage { get; set; }

        public void SetSelected()
        {
            GameButton.BorderThickness = new Thickness(3);
        }
        public Button GameButton { get; set; }

        public void DisableElement()
        {
            GameButton.Background = Brushes.White;
        }

        public void Update()
        {
            GameButton.BorderThickness = new Thickness(1);
        }
    }
}
