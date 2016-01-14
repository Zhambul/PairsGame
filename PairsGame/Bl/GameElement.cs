using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PairsGame.Annotations;

namespace PairsGame.Bl
{
    public class GameElement : INotifyPropertyChanged, IGameElement
    {

        private ButtonBackground _frontBackground;
        public ButtonBackground FrontImage
        {
            get { return _frontBackground; }
            set
            {
                _frontBackground = value;
                OnPropertyChanged();
            }
        }
        public int Row { get; set; }
        public int Col { get; set; }
        public bool Empty { get; set; }
        public bool Disabled { get; set; }
        public bool Checked { get; set; }
        public void SetSelected()
        {
            GameButton.BorderThickness = new Thickness(3);
        }
        public Button GameButton { get; set; }

        public void DisableElement()
        {
            Empty = true;
            Checked = true;
            Disabled = true;
            GameButton.Background = Brushes.White;
        }

        public void Update()
        {
            GameButton.BorderThickness = new Thickness(1);
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
