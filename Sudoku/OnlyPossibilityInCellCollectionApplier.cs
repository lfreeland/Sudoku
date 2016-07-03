using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class OnlyPossibilityInCellCollectionApplier
    {
        private Board _board;

        public OnlyPossibilityInCellCollectionApplier(Board board)
        {
            _board = board;
        }

        public void Apply()
        {
            Possibilities possibilities = new Possibilities();

            foreach (int possibility in possibilities.Values)
            {
                // if the possibility is only in one cell in the row, column, or square,
                // then that is the value for that cell.

                foreach (CellCollection cc in _board.AllCellCollections)
                {
                    List<Cell> possibilityCells = cc.Cells.Where(c => c.Possibilities.Values.Contains(possibility)).ToList();

                    if (possibilityCells.Count == 1)
                    {
                        _board.setCellValue(possibilityCells[0], possibility);
                    }
                }
            }
        }
    }
}
