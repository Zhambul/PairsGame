using System;
using System.Collections.Generic;
using System.Drawing;
using System.Resources;
using PairsGame.Properties;

namespace PairsGame.Bl
{
    class ImageFabric : IImageFabric
    {
        private readonly List<ButtonBackground> _allImages;
        private readonly int _size;
        public ImageFabric(int size)
        {
            _size = size;
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
                for (int j = 0; j < _size; j++)
                {
                    var rm = Resources.ResourceManager;
                    string intString;
                    if (j < 10)
                    {
                        intString = "0" + j;
                    }
                    else
                    {
                        intString = Convert.ToString(j);
                    }
                    var sad = string.Format("t{0}", intString);
                    var myImage = (Bitmap)rm.GetObject(sad);
                    Util.Convert(myImage);

                    _allImages.Add(new ButtonBackground(Util.Convert(myImage), j));
                }
            }
        }

    }
}
