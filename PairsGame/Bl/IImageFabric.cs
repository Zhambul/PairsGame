using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PairsGame.Bl
{
    public interface IImageFabric
    {
        ButtonBackground GetRandomImage();
    }
}
