using System.Collections.Generic;

namespace Sudoku
{
    public class Square : CellCollection
    {
        public int RowStart { get; private set; }

        public int RowEnd { get; private set; }

        public int ColumnStart { get; private set; }

        public int ColumnEnd { get; private set; }

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