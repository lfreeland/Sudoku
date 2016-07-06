using System.Collections.Generic;
using System.Linq;

using Sudoku.Model;

namespace Sudoku.Solver
{
    /// <summary>
    /// This solver identifies a value that a cell can be by enumerating
    /// all the possible values. If the possibility is in only one cell
    /// in the row, column or square, then that is the value for that cell.
    /// </summary>
    public class OnlyPossibilityInCellCollectionSolver : SolverBase
    {
        public override void Solve(Board board)
        {
            Possibilities possibilities = new Possibilities();

            foreach (int possibility in possibilities.Values)
            {
                // if the possibility is only in one cell in the row, column, or square,
                // then that is the value for that cell.

                foreach (CellCollection cc in board.AllCellCollections)
                {
                    List<Cell> possibilityCells = cc.UnsolvedCells.Where(c => c.Possibilities.Values.Contains(possibility)).ToList();

                    if (possibilityCells.Count == 1)
                    {
                        board.setCellValue(possibilityCells[0], possibility);
                    }
                }
            }
        }
    }
}
