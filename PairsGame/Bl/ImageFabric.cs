using System;
using System.Collections.Generic;
using System.Drawing;
using System.Resources;
using PairsGame.Properties;

namespace PairsGame.Bl
{
    // класс для создания фотографий
    class ImageFabric : IImageFabric
    {
        // коллекция всех фоток
        private readonly List<ButtonBackground> _allImages;
        private readonly int _size;
        public ImageFabric(int size)
        {
            _size = size;
            _allImages = new List<ButtonBackground>();
            
            // инициализация всех фотогрфий
            InitImages();    
        }

        // возрващение рандомной фотки с последующим удалением из коллекции
        public ButtonBackground GetRandomImage()
        {
            Random random = new Random();
            int generatedIndex =  random.Next(_allImages.Count - 1);
            var color = _allImages[generatedIndex];
            _allImages.RemoveAt(generatedIndex);

            return color;
        }

        // инициализация в зависимости от величины игры
        private void InitImages()
        {
            // делаем цикла 2 раза, ибо пара нужна
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    // ресурсмэнеджер - нужна для того чтобы брать ресура (фотографию) по ее имени
                    var rm = Resources.ResourceManager;
                    // конструируем имя
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
                    // берем фотку
                    var myImage = (Bitmap)rm.GetObject(sad);
                    Util.Convert(myImage);
                    // добавляем в коллекцию фоток
                    _allImages.Add(new ButtonBackground(Util.Convert(myImage), j));
                }
            }
        }

    }
}
