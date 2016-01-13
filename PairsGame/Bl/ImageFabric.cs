using System;
using System.Collections.Generic;
using PairsGame.Properties;

namespace PairsGame.Bl
{
    class ImageFabric : IImageFabric
    {
        private readonly List<ButtonBackground> _allImages;

        public ImageFabric()
        {
            _allImages = new List<ButtonBackground>();
            
            InitImages();    
        }

        public ButtonBackground GetRandomImage()
        {
            Random random = new Random();
            int generatedIndex =  random.Next(_allImages.Count - 1);
            var color = _allImages[generatedIndex];
            _allImages.RemoveAt(generatedIndex);

            return color;
        }

        private void InitImages()
        {
            for (int i = 0; i < 2; i++)
            {
                _allImages.Add(new ButtonBackground(Util.Convert(Resources._1), 0));
                _allImages.Add(new ButtonBackground(Util.Convert(Resources._2), 1));
                _allImages.Add(new ButtonBackground(Util.Convert(Resources._3), 2));
                _allImages.Add(new ButtonBackground(Util.Convert(Resources._4), 3));
                _allImages.Add(new ButtonBackground(Util.Convert(Resources._5), 4));
                _allImages.Add(new ButtonBackground(Util.Convert(Resources._6), 5));
                _allImages.Add(new ButtonBackground(Util.Convert(Resources._7), 6));
                _allImages.Add(new ButtonBackground(Util.Convert(Resources._8), 7));
            }
        }

    }
}
