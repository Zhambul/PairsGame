using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PairsGame.Bl
{
    // интерфейс для фабрики фотографий
    public interface IImageFabric
    {
        // метод для создания фотографий
        ButtonBackground GetRandomImage();
    }
}
