using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairsGame.Bl
{
    class Route
    {
        int Turns { get; set; }

        private RouteHistory _history;

        public Route(IGameElement gameElement)
        {

        }

        public Route(RouteHistory history)
        {
            _history = history;
        }
        public void Split()
        {
            
        }
    }
}
