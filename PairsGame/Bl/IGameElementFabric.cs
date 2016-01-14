using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairsGame.Bl
{
    // интерфейс для создания элементов игры
    interface IGameElementFabric
    {
        IGameElement Get();
    }
}
