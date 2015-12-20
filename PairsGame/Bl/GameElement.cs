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
        private ButtonBackground _backBackground;
        public ButtonBackground BackBackground
        {
            get { return _backBackground; }
            set
            {
                _backBackground = value;
                OnPropertyChanged();
            }
        }

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

        public Button GameButton { get; set; }

        private bool _isUpFront;
        public GameElement()
        {
            InitBackImage();

            GameButton = new Button()
            {
                Background = BackBackground.Image
            };
        }

        private void InitBackImage()
        {
            _backBackground = new ButtonBackground(
                Util.Convert(Resources.white),-1    
            );
        }

        public void Flip()
        {
            GameButton.Background = _isUpFront ? BackBackground.Image : FrontImage.Image;
            _isUpFront = !_isUpFront;
        }

        public void FlipFrontDown()
        {
            GameButton.Background = BackBackground.Image;
            _isUpFront = false;
        }
     

        public void DisableElement()
        {
            GameButton.Background = Brushes.Black;
        }

        public int index { get { return 0; } set{} }

        public string Name { get { return "asdqwe"; }
            set { }
        }

        public void Update()
        {
            FlipFrontDown();
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
