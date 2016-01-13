using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace PairsGame.Bl
{
    public class GameElementFabric : IGameElementFabric
    {
        private readonly IImageFabric _imageFabric;

        public GameElementFabric(IImageFabric imageFabric)
        {
            _imageFabric = imageFabric;
        }
        public IGameElement GetGameElement()
        {
            var frontImage = _imageFabric.GetRandomImage();
            return new GameElement
            {   
                Empty = true,
                Checked = false,
                Disabled = false,
                FrontImage = frontImage,
                GameButton = new Button
                {
                    Background = frontImage.Image,
                    BorderBrush = Brushes.Black
                }
            };
        }
    }
}