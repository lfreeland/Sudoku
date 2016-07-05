using Sudoku.Model;

namespace Sudoku.Solver
{
    /// <summary>
    /// This solver enumerates all the cells on the board and if a cell
    /// only has one possibility, then that is the value for the cell.
    /// </summary>
    public class SingleCellPossibilitySolver : SolverBase
    {
        public override void Solve(Board board)
        {
            foreach (Row row in board.Rows)
            {
                foreach (Cell cell in row.Cells)
                {
                    if (cell.Value.HasValue == false &&
                        cell.Possibilities.Count == 1)
                    {
                        board.setCellValue(cell, cell.Possibilities.Values[0]);
                    }
                }
            }
        }
    }
}
