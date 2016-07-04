using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Board
    {
        public IReadOnlyList<Row> Rows { get; private set; }
        public IReadOnlyList<Column> Columns { get; private set; }
        public IReadOnlyList<Square> Squares { get; private set; }

        public IReadOnlyCollection<CellCollection> AllCellCollections { get; private set; }

        public Board()
        {
            List<Row> rows = new List<Row>();
            List<Column> columns = new List<Column>();

            for (int rowNum = 0; rowNum < 9; ++rowNum)
            {
                rows.Add(new Row(rowNum));
            }

            for (int colNumber = 0; colNumber < 9; ++colNumber)
            {
                Column c = new Column();

                foreach (Row r in rows)
                {
                    c.Cells.Add(r.Cells[colNumber]);
                }

                columns.Add(c);
            }

            Columns = columns;
            Rows = rows;

            Square topLeft = new Square(0, 2, 0, 2, rows);
            Square topMiddle = new Square(0, 2, 3, 5, rows);
            Square topRight = new Square(0, 2, 6, 8, rows);
            Square middleLeft = new Square(3, 5, 0, 2, rows);
            Square center = new Square(3, 5, 3, 5, rows);
            Square middleRight = new Square(3, 5, 6, 8, rows);
            Square bottomLeft = new Square(6, 8, 0, 2, rows);
            Square bottomMiddle = new Square(6, 8, 3, 5, rows);
            Square bottomRight = new Square(6, 8, 6, 8, rows);

            Squares = new List<Square> { topLeft, topMiddle, topRight, middleLeft, center, middleRight, bottomLeft, bottomMiddle, bottomRight };

            List<CellCollection> allCollections = new List<CellCollection>();
            allCollections.AddRange(Rows);
            allCollections.AddRange(Columns);
            allCollections.AddRange(Squares);

            AllCellCollections = allCollections;
        }

        public Board(String boardString)
            :this()
        {
            if (String.IsNullOrWhiteSpace(boardString))
            {
                throw new ArgumentNullException("The boardString is null or empty.");
            }

            if (boardString.Length != 81)
            {
                throw new ApplicationException("Expected the board string to have 81 characters representing 81 cells. There are " + boardString.Length + " characters.");
            }

            for (int i = 0; i < boardString.Length; ++i)
            {
                int row = i / 9;
                int column = i % 9;

                char valueChar = boardString[i];

                if (valueChar != '0')
                {
                    int value = int.Parse(valueChar.ToString());
                    setCellValue(row, column, value);
                }
            }
        }

        public void setCellValue(int rowNum, int colNum, int value)
        {
            Cell cell = Rows[rowNum].Cells[colNum];
            setCellValue(cell, value);
        }

        public void setCellValue(Cell cell, int value)
        {
            cell.Value = value;
            cell.Square.RemovePossibility(value);

            Row row = Rows[cell.Row];
            row.RemovePossibility(value);

            Column column = Columns[cell.Column];
            column.RemovePossibility(value);
        }

        public int remainingCellsToSolveCount()
        {
            int count = 0;

            foreach (Row r in Rows)
            {
                foreach (Cell c in r.Cells)
                {
                    if (c.Value.HasValue == false)
                    {
                        ++count;
                    }
                }
            }

            return count;
        }

        public bool isSolved()
        {
            int count = remainingCellsToSolveCount();
            return count == 0;
        }

        public void printValuesToOutput()
        {
            foreach (Row r in Rows)
            {
                foreach (Cell c in r.Cells)
                {
                    String valueStr = c.Value.HasValue ? c.Value.ToString() : "0";
                    valueStr += " ";

                    Debug.Write(valueStr);
                }

                Debug.Write(Environment.NewLine);
            }
        }
    }
}
