using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PairsGame.Bl
{
    interface IImageFabric
    {
        ButtonBackground GetRandomImage();
    }
}
