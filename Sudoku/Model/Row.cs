using System.Collections.Generic;

using Sudoku.Model;

namespace Sudoku.Model
{
    /// <summary>
    /// A row of cells on a Sudoku board.
    /// </summary>
    public class Row : CellCollection
    {
        /// <summary>
        /// The zero-based row number of this row on the Sudoku board.
        /// </summary>
        public int RowNumber { get; private set; }

        /// <summary>
        /// Constructs a row of cells with the given row number.
        /// The cells in the row are instantiated.
        /// </summary>
        /// <param name="rowNumber"></param>
        public Row(int rowNumber)
        {
            RowNumber = rowNumber;

            List<Cell> myCells = new List<Cell>();

            for (int i = 0; i < 9; ++i)
            {
                Cell c = new Cell();
                c.Row = RowNumber;
                c.Column = i;

                myCells.Add(c);
            }

            Cells = myCells;
        }

        /// <summary>
        /// Creates a generic row with a row number of zero.
        /// </summary>
        public Row()
           : this(0)
        {

        }
    }
}
