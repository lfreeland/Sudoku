using System.Collections.Generic;

using Sudoku.Model;

namespace Sudoku.Model
{
    /// <summary>
    /// The 3x3 square on a 9x9 Sudoku board.
    /// </summary>
    /// <remarks>The cells are not instantiated because they are populated using cells from the given rows.</remarks>
    public class Square : CellCollection
    {
        /// <summary>
        /// The zero-based number of the row starting in this square.
        /// </summary>
        public int RowStart { get; private set; }

        /// <summary>
        /// The zero-based number of the row ending in this square.
        /// </summary>
        public int RowEnd { get; private set; }

        /// <summary>
        /// The zero-based number of the column starting in this square.
        /// </summary>
        public int ColumnStart { get; private set; }

        /// <summary>
        /// The zero-based number of the column ending in this square.
        /// </summary>
        public int ColumnEnd { get; private set; }

        /// <summary>
        /// The rows of cells in this square.
        /// </summary>
        public List<Row> Rows { get; private set; }

        public Square(int rowStart, int rowEnd, int columnStart, int columnEnd, List<Row> rows)
        {
            RowStart = rowStart;
            RowEnd = rowEnd;
            ColumnStart = columnStart;
            ColumnEnd = columnEnd;

            Cells = new List<Cell>();

            Rows = new List<Row>();

            for (int rowNum = rowStart; rowNum <= rowEnd; ++rowNum)
            {
                Row r = rows[rowNum];

                Row squareRow = new Row(rowNum);

                for (int columnNum = columnStart; columnNum <= columnEnd; ++columnNum)
                {
                    Cell c = r.Cells[columnNum];
                    c.Square = this;

                    Cells.Add(c);

                    squareRow.Cells.Add(c);
                }

                Rows.Add(squareRow);
            }
        }
    }
}