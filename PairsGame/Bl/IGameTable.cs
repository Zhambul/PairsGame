using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace PairsGame.Bl
{
    public interface IGameTable
    {
        ObservableCollection<IGameElement> GameElements { get; set; }
        void RegisterGameElement(IGameElement gameElement, int i, int j);
        void RemoveGameElement(IGameElement gameElement);
        void NotifyGameElements();
        void MakeMove(Button button);
        void FillWithElements(int size, UniformGrid myGrid);
    }
}
