using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PairsGame.Bl
{
    // класс для внешнего вида Элемента игры. id нужен для идентификации каждого элемента
    public class ButtonBackground
    {
        public ImageBrush Image { get; set; }

        public int Id { get; set; }

        public ButtonBackground(ImageBrush image, int id)
        {
            Image = image;
            Id = id;
        }
    }
}
