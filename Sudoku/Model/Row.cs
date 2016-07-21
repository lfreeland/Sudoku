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
        /// Constructs a row of 9 cells with the given row number.
        /// The cells in the row are instantiated.
        /// </summary>
        /// <param name="rowNumber">The row number.</param>
        public Row(int rowNumber)
            : this(rowNumber, 9)
        {
            
        }

        /// <summary>
        /// Constructs a row of numCells with the given row number.
        /// </summary>
        /// <param name="rowNumber">The row number.</param>
        /// <param name="numCells">The number of cells in the row.</param>
        public Row(int rowNumber, int numCells)
        {
            RowNumber = rowNumber;

            List<Cell> myCells = new List<Cell>();

            for (int i = 0; i < numCells; ++i)
            {
                Cell c = new Cell(numCells);
                c.Row = RowNumber;
                c.Column = i;

                myCells.Add(c);
            }

            Cells = myCells;
        }

        /// <summary>
        /// Creates a generic row with 9 cells with a row number of zero.
        /// </summary>
        public Row()
           : this(0)
        {

        }
    }
}
