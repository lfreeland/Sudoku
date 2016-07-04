using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Solver
    {
        private Board _board = null;

        public Solver(Board board)
        {
            _board = board;
        }

        public void Solve()
        {
            SingleCellPossibilityApplier scpa = new SingleCellPossibilityApplier(_board);
            OnlyPossibilityInCellCollectionApplier opicca = new OnlyPossibilityInCellCollectionApplier(_board);

            int remainingCellsToSolveCount = _board.remainingCellsToSolveCount();
            Boolean cellsSet = true;

            while (remainingCellsToSolveCount > 0 &&
                   cellsSet)
            {
                scpa.Apply();
                opicca.Apply();

                int newRemainingCellsSet = _board.remainingCellsToSolveCount();
                int numCellsSet = remainingCellsToSolveCount - newRemainingCellsSet;
                remainingCellsToSolveCount = newRemainingCellsSet;

                cellsSet = numCellsSet > 0;
            }
        }
    }
}
