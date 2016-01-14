using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairsGame.Bl
{
    class GameElementStackFabric : IGameElementsStackFabric
    {
        private readonly IGameElementFabric _gameElementFabric;

        private const int NumberOfLayers = 2;
        
        public GameElementStackFabric(IGameElementFabric gameElementFabric)
        {
            _gameElementFabric = gameElementFabric;
        }

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
