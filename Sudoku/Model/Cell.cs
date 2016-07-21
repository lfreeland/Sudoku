using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Model
{
    /// <summary>
    /// Represents a cell on a Sudoku board containing the possibilities that the value can be
    /// as well as the Value, if there is one. It also has its coordinates, row and column, on
    /// the board.
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// The possible values that the cell can be.
        /// </summary>
        public Possibilities Possibilities { get; private set; }

        /// <summary>
        /// The zero-based row number that this cell is in on the board.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// The zero-based column number that this cell is in on the board.
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// The square that this cell is in on the board.
        /// </summary>
        public Square Square { get; set; }

        private int? val = null;

        /// <summary>
        /// The value of this cell, if there is one.
        /// </summary>
        public int? Value
        {
            get
            {
                return val;
            }
            set
            {
                val = value;

                Possibilities.Values.Clear();
            }
        }

        /// <summary>
        /// Creates a cell with the specified number of possibilities in it.
        /// </summary>
        /// <param name="numPossibilities">The number of possibilities in the cell from 1 to numPossibilities.</param>
        public Cell(int numPossibilities)
        {
            Possibilities = new Possibilities(numPossibilities);
        }
    }
}
