using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairsGame.Bl
{
    class RouteHelper
    {
        private List<Route> _possibleRoutes;

        public RouteHelper()
        {
            _possibleRoutes = new List<Route>();
        }

        public bool MakeRoute(GameElement[,] allElements, IGameElement currentElement, IGameElement elementToGet)
        {
            var neighbourElements = GetNeigbourElements(allElements, currentElement);

            var emptyNeightboursCount = GetEmptyCheckableNeighboursCount(neighbourElements);
            for (int i = 0; i < emptyNeightboursCount; i++)
            {
                foreach (var neighbourElement in neighbourElements)
                {
                    if (neighbourElement != null && neighbourElement.Empty && !neighbourElement.Checked)
                    {
                        neighbourElement.Checked = true;

                        foreach (var elementToCheck in GetNeigbourElements(allElements, neighbourElement))
                        {
                            if (elementToCheck == elementToGet)
                            {
                                Debug.WriteLine("got it");
                                break;
                            }
                        }
                        MakeRoute(allElements, neighbourElement, elementToGet);
                    }
                }

            }
            return false;
        }

        private static void SetUnchecked(GameElement[,] allElements)
        {
            for (int row = 0; row < allElements.GetLength(0); row++)
            {
                for (int col = 0; col < allElements.GetLength(1); col++)
                {
                    allElements[row, col].Checked = false;
                }
            }
        }

        private int GetEmptyCheckableNeighboursCount(GameElement[] neighbourElements)
        {
            return neighbourElements.Count(neighbourElement => neighbourElement != null && neighbourElement.Empty && !neighbourElement.Checked);
        }

        private GameElement[] GetNeigbourElements(GameElement[,] allElements, IGameElement currentElement)
        {
            var neighbourElements = new GameElement[4];

            neighbourElements[0] = GetLeftElement(allElements, currentElement.Row, currentElement.Col);
            neighbourElements[1] = GetUpElement(allElements, currentElement.Row, currentElement.Col);
            neighbourElements[2] = GetRightElement(allElements, currentElement.Row, currentElement.Col);
            neighbourElements[3] = GetDownElement(allElements, currentElement.Row, currentElement.Col);

            return neighbourElements;
        }
        private GameElement GetLeftElement(GameElement[,] allElements, int startRow, int startCol)
        {
            if (startCol == 0)
            {
                return null;
            }
            return allElements[startRow, startCol - 1];
        }
        private GameElement GetRightElement(GameElement[,] allElements, int startRow, int startCol)
        {
            if (startCol == allElements.GetLength(0) - 1)
            {
                return null;
            }   
            return allElements[startRow, startCol + 1];
        }
        private GameElement GetDownElement(GameElement[,] allElements, int startRow, int startCol)
        {
            if (startRow == allElements.GetLength(0) - 1 )
            {
                return null;
            } 
            return allElements[startRow + 1, startCol];
        }
        private GameElement GetUpElement(GameElement[,] allElements, int startRow, int startCol)
        {
            if (startRow == 0)
            {
                return null;
            }
            return allElements[startRow - 1, startCol];
        }
    }
}
