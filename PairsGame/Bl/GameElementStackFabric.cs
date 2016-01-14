using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairsGame.Bl
{
    // класс для создания стака элементов
    class GameElementStackFabric : IGameElementsStackFabric
    {
        private readonly IGameElementFabric _gameElementFabric;

        private const int NumberOfLayers = 2;
        
        public GameElementStackFabric(IGameElementFabric gameElementFabric)
        {
            _gameElementFabric = gameElementFabric;
        }

        // главный метод
        public GameElementsStack Get()
        {
            var result = new GameElementsStack {Elements = new Stack<IGameElement>()};

            for (var i = 0; i < NumberOfLayers; i++)
            {
                result.Elements.Push(_gameElementFabric.Get());
            }

            return result;
        }
    }
}
