using System;
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

        /// <summary>
        /// Creates the square with the specified row and column dimensions. After the rows are created,
        /// invoke PopulateRows with the created rows so that the squares are properly initialized.
        /// </summary>
        /// <param name="rowStart">The zero-based number of the row starting in this square.</param>
        /// <param name="rowEnd">The zero-based number of the row ending in this square.</param>
        /// <param name="columnStart">The zero-based number of the column starting in this square.</param>
        /// <param name="columnEnd">The zero-based number of the column ending in this square.</param>
        public Square(int rowStart, int rowEnd, int columnStart, int columnEnd)
        {
            RowStart = rowStart;
            RowEnd = rowEnd;
            ColumnStart = columnStart;
            ColumnEnd = columnEnd;

            Cells = new List<Cell>();

            Rows = new List<Row>();
        }

        /// <summary>
        /// Creates the square with the specified row and column dimensions and initializes the square with the
        /// rows given.
        /// </summary>
        /// <param name="rowStart">The zero-based number of the row starting in this square.</param>
        /// <param name="rowEnd">The zero-based number of the row ending in this square.</param>
        /// <param name="columnStart">The zero-based number of the column starting in this square.</param>
        /// <param name="columnEnd">The zero-based number of the column ending in this square.</param>
        /// <param name="rows">The board rows used to populate this square.</param>
        public Square(int rowStart, int rowEnd, int columnStart, int columnEnd, List<Row> rows)
            : this(rowStart, rowEnd, columnStart, columnEnd)
        {
            PopulateRows(rows);
        }

        /// <summary>
        /// Populates the squares rows using the board's rows passed in.
        /// </summary>
        /// <param name="rows">The board's rows.</param>
        public void PopulateRows(IReadOnlyList<Row> rows)
        {
            for (int rowNum = RowStart; rowNum <= RowEnd; ++rowNum)
            {
                Row r = rows[rowNum];

                Row squareRow = new Row(rowNum);

                for (int columnNum = ColumnStart; columnNum <= ColumnEnd; ++columnNum)
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