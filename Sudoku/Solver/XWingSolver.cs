using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Model;

namespace Sudoku.Solver
{
    /// <summary>
    /// This solver uses the row and column X-Wing strategy presented at
    /// http://www.sudokuwiki.org/X_Wing_Strategy
    /// </summary>
    public class XWingSolver : SolverBase
    {
        public override void Solve(Board board)
        {
            // Rows X-Wing
            foreach (Row r in board.Rows)
            {
                foreach (int rowPossibility in r.Possibilities.Values)
                {
                    List<Cell> rowcellsWithPossibilities = r.getCellsWithPossibility(rowPossibility);

                    if (rowcellsWithPossibilities.Count != 2)
                    {
                        continue;
                    }

                    foreach (Row r2 in board.Rows)
                    {
                        if (r2 == r)
                        {
                            continue;
                        }

                        List<Cell> row2cellsWithPossibilities = r2.getCellsWithPossibility(rowPossibility);

                        if (row2cellsWithPossibilities.Count == 2 &&
                            rowcellsWithPossibilities[0].Column == row2cellsWithPossibilities[0].Column &&
                            rowcellsWithPossibilities[1].Column == row2cellsWithPossibilities[1].Column)
                        {
                            List<Cell> columnOneCells = new List<Cell> { rowcellsWithPossibilities[0], row2cellsWithPossibilities[0] };

                            board.Columns[rowcellsWithPossibilities[0].Column].RemovePossibility(rowPossibility, columnOneCells);

                            List<Cell> columnTwoCells = new List<Cell> { rowcellsWithPossibilities[1], row2cellsWithPossibilities[1] };

                            board.Columns[rowcellsWithPossibilities[1].Column].RemovePossibility(rowPossibility, columnTwoCells);
                        }
                    }
                }
            }

            // Columns X-Wing
            foreach (Column c in board.Columns)
            {
                foreach (int columnPossibility in c.Possibilities.Values)
                {
                    List<Cell> columnCellsWithPossibilities = c.getCellsWithPossibility(columnPossibility);

                    if (columnCellsWithPossibilities.Count != 2)
                    {
                        continue;
                    }

                    foreach (Column c2 in board.Columns)
                    {
                        if (c2 == c)
                        {
                            continue;
                        }

                        List<Cell> column2CellsWithPossibilities = c2.getCellsWithPossibility(columnPossibility);

                        if (column2CellsWithPossibilities.Count == 2 &&
                            columnCellsWithPossibilities[0].Row == column2CellsWithPossibilities[0].Row &&
                            columnCellsWithPossibilities[1].Row == column2CellsWithPossibilities[1].Row)
                        {
                            List<Cell> rowOneCells = new List<Cell> { columnCellsWithPossibilities[0], column2CellsWithPossibilities[0] };

                            board.Rows[columnCellsWithPossibilities[0].Row].RemovePossibility(columnPossibility, rowOneCells);

                            List<Cell> rowTwoCells = new List<Cell> { columnCellsWithPossibilities[1], column2CellsWithPossibilities[1] };

                            board.Rows[columnCellsWithPossibilities[1].Row].RemovePossibility(columnPossibility, rowTwoCells);
                        }
                    }
                }
            }
        }
    }
}
