using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using PairsGame.Annotations;
using PairsGame.Properties;

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
            GameButton.BorderThickness = new Thickness(0);
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
