using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class SquareLineReducer
    {
        private Board _board;

        public SquareLineReducer(Board board)
        {
            _board = board;
        }

        public void Apply()
        {
            // If a row or column in a square only has a possibility in it,
            // remove the possibilities from the rest of the row or column in the 
            // other squares in that row or column because the possibilitiy
            // must be in that row or column in the square.

            Possibilities possibilities = new Possibilities();

            foreach (int possibility in possibilities.Values)
            {
                foreach (Square sq in _board.Squares)
                {
                    HashSet<int> rowsContainingPossibility = new HashSet<int>();
                    HashSet<int> columnsContainingPossibility = new HashSet<int>();

                    foreach (Cell c in sq.Cells)
                    {
                        if (c.Possibilities.Values.Contains(possibility))
                        {
                            rowsContainingPossibility.Add(c.Row);
                            columnsContainingPossibility.Add(c.Column);
                        }
                    }

                    if (rowsContainingPossibility.Count == 1)
                    {
                        Row row = _board.Rows[rowsContainingPossibility.First()];

                        foreach (Cell rowCell in row.Cells)
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
                        Column column = _board.Columns[columnsContainingPossibility.First()];

                        foreach (Cell columnCell in column.Cells)
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
