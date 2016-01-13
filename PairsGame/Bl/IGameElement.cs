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
        bool Empty { get; set; }
        bool Checked { get; set; }
        bool Disabled { get; set; }
        int Row { get; set; }
        int Col { get; set; }
        ButtonBackground FrontImage { get; set; }
        Button GameButton { get; set; }
        void DisableElement();
        void SetSelected();
    }
}
