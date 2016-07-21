using System.Collections.Generic;
using System.Linq;

using Sudoku.Model;

namespace Sudoku.Solver
{
    /// <summary>
    /// If a row or column in a square only has a possibility in it,
    /// remove the possibilities from the rest of the row or column in the
    /// other squares in that row or column because the possibilitiy
    /// must be in that row or column in the square.
    /// </summary>
    public class SquareLineReducerSolver : SolverBase
    {
        public override void Solve(Board board)
        {
            Possibilities possibilities = new Possibilities(board.Rows.Count);

            foreach (int possibility in possibilities.Values)
            {
                foreach (Square sq in board.Squares)
                {
                    HashSet<int> rowsContainingPossibility = new HashSet<int>();
                    HashSet<int> columnsContainingPossibility = new HashSet<int>();

                    foreach (Cell c in sq.UnsolvedCells)
                    {
                        if (c.Possibilities.Values.Contains(possibility))
                        {
                            rowsContainingPossibility.Add(c.Row);
                            columnsContainingPossibility.Add(c.Column);
                        }
                    }

                    if (rowsContainingPossibility.Count == 1)
                    {
                        Row row = board.Rows[rowsContainingPossibility.First()];

                        foreach (Cell rowCell in row.UnsolvedCells)
                        {
                            if (rowCell.Column >= sq.ColumnStart &&
                                rowCell.Column <= sq.ColumnEnd)
                            {
                                continue;
                            }

                            rowCell.Possibilities.Remove(possibility);
                        }
                    }

                    if (columnsContainingPossibility.Count == 1)
                    {
                        Column column = board.Columns[columnsContainingPossibility.First()];

                        foreach (Cell columnCell in column.UnsolvedCells)
                        {
                            if (columnCell.Row >= sq.RowStart &&
                                columnCell.Row <= sq.RowEnd)
                            {
                                continue;
                            }

                            columnCell.Possibilities.Remove(possibility);
                        }
                    }
                }
            }
        }
    }
}
