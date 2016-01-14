using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PairsGame.Bl
{
    public interface IGameElement
    {
        void Update();
        ButtonBackground FrontImage { get; set; }
        Button GameButton { get; set; }
        void DisableElement();
        void SetSelected();
    }
}
