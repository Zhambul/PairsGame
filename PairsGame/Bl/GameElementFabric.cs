using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairsGame.Bl
{
    public class GameElementFabric : IGameElementFabric
    {
        public IGameElement GetGameElement()
        {
            return new GameElement();
        }
    }
}