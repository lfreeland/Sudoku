using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Row : CellCollection
    {
        public int RowNumber { get; private set; }

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
    }
}
