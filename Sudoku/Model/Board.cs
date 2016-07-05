using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Model
{
    /// <summary>
    /// Represents a 9x9 Sudoku Board.
    /// </summary>
    public class Board
    {
        /// <summary>
        /// The 9 horizontal rows on the board. Each row contains 9 cells.
        /// </summary>
        public IReadOnlyList<Row> Rows { get; private set; }

        /// <summary>
        /// The 9 vertical columns on the board.  Each column contains 9 cells.
        /// </summary>
        public IReadOnlyList<Column> Columns { get; private set; }

        /// <summary>
        /// The 9 3x3 squares on the board. Each square contains 9 cells.
        /// </summary>
        public IReadOnlyList<Square> Squares { get; private set; }

        /// <summary>
        /// All the rows, columns, and squares on the board.
        /// </summary>
        public IReadOnlyCollection<CellCollection> AllCellCollections { get; private set; }

        /// <summary>
        /// Creates the Rows, Columns, and Squares on the board. The Rows, Columns, 
        /// and Squares share the cells among them so that when the Value or Possibilities
        /// are changed, the row, column and square containing that cell see the changes.
        /// </summary>
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

        /// <summary>
        /// Constructs a board using an 81 digit string representation of the board.
        /// The representation is constructed by reading a 9x9 board row by row
        /// from left to right starting at the top row and going down. If the cell
        /// has no value, enter 0. Otherwise, enter the 1-9 digit that's in that cell.
        /// </summary>
        /// <param name="boardRepresentation"></param>
        public Board(String boardRepresentation)
            :this()
        {
            if (String.IsNullOrWhiteSpace(boardRepresentation))
            {
                throw new ArgumentNullException("The boardRepresentation is null or empty.");
            }

            if (boardRepresentation.Length != 81)
            {
                throw new ApplicationException("Expected the board representation to have 81 characters representing 81 cells. There are " + boardRepresentation.Length + " characters.");
            }

            for (int i = 0; i < boardRepresentation.Length; ++i)
            {
                int row = i / 9;
                int column = i % 9;

                char valueChar = boardRepresentation[i];

                if (valueChar != '0')
                {
                    int value = int.Parse(valueChar.ToString());
                    setCellValue(row, column, value);
                }
            }
        }

        /// <summary>
        /// Sets the cell's value with the given value in the given row and column.
        /// </summary>
        /// <param name="rowNum">The zero-based number of the row the cell is in.</param>
        /// <param name="colNum">The zero-based number of the column the cell is in.</param>
        /// <param name="value">The value that the cell should be. The value can be 1-9.</param>
        public void setCellValue(int rowNum, int colNum, int value)
        {
            Cell cell = Rows[rowNum].Cells[colNum];
            setCellValue(cell, value);
        }

        /// <summary>
        /// Sets the given cell's value with the given value.
        /// </summary>
        /// <param name="cell">The cell whose value to set.</param>
        /// <param name="value">The value that the cell should be. The value can be 1-9.</param>
        public void setCellValue(Cell cell, int value)
        {
            cell.Value = value;
            cell.Square.RemovePossibility(value);

            Row row = Rows[cell.Row];
            row.RemovePossibility(value);

            Column column = Columns[cell.Column];
            column.RemovePossibility(value);
        }

        /// <summary>
        /// The number of remaining cells to solve aka
        /// the number of cells without a value.
        /// </summary>
        /// <returns>The number of cells without a value.</returns>
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

        /// <summary>
        /// Returns the number of total remaining possibilities on the board by
        /// summing each cell's number of possibilities. Useful to determine
        /// if progress has been made in solving the board or not.
        /// </summary>
        /// <returns>The number of total remaining possibilities on the board</returns>
        public int remainingPossibilitiesCount()
        {
            int count = 0;

            foreach (Row r in Rows)
            {
                foreach (Cell c in r.Cells)
                {
                    if (c.Value.HasValue == false)
                    {
                        count += c.Possibilities.Count;
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// Returns true when the board has been solved.
        /// I.E. all the cells have a value.
        /// </summary>
        /// <returns>True when all the cells have a value.</returns>
        public bool isSolved()
        {
            int count = remainingCellsToSolveCount();
            return count == 0;
        }

        /// <summary>
        /// Outputs the 9x9 board with its values to the Debug stream with the heading "VALUES".
        /// Next it outputs the 9x9 board with its "POSSIBILITIES" in each cell to the Debug Stream.
        /// This is helpful for diagnostic purposes.
        /// </summary>
        public void printDiagnosticsToOutput()
        {
            Debug.WriteLine("VALUES");
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

            Debug.WriteLine("POSSIBILITIES");
            foreach (Row r in Rows)
            {
                foreach (Cell c in r.Cells)
                {
                    String possibilitiesStr = "{" + String.Join(",", c.Possibilities.Values) + "}";
                    possibilitiesStr += " ";

                    Debug.Write(possibilitiesStr);
                }

                Debug.Write(Environment.NewLine);
            }
        }
    }
}
