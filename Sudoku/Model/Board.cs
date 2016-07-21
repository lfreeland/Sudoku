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
        /// The horizontal rows on the board. Each row contains cells.
        /// </summary>
        public IReadOnlyList<Row> Rows { get; private set; }

        /// <summary>
        /// The vertical columns on the board.  Each column contains cells.
        /// </summary>
        public IReadOnlyList<Column> Columns { get; private set; }

        /// <summary>
        /// The squares on the board. Each square contains cells.
        /// </summary>
        public IReadOnlyList<Square> Squares { get; private set; }

        /// <summary>
        /// All the rows, columns, and squares on the board.
        /// </summary>
        public IReadOnlyCollection<CellCollection> AllCellCollections { get; private set; }

        /// <summary>
        /// Creates 9 Rows, 9 Columns, and 9 3x3 Squares on the board. The Rows, Columns, 
        /// and Squares share the cells among them so that when the Value or Possibilities
        /// are changed, the row, column and square containing that cell see the changes.
        /// </summary>
        public Board()
            : this(9, new List<Square>())
        {
            Square topLeft = new Square(0, 2, 0, 2);
            Square topMiddle = new Square(0, 2, 3, 5);
            Square topRight = new Square(0, 2, 6, 8);
            Square middleLeft = new Square(3, 5, 0, 2);
            Square center = new Square(3, 5, 3, 5);
            Square middleRight = new Square(3, 5, 6, 8);
            Square bottomLeft = new Square(6, 8, 0, 2);
            Square bottomMiddle = new Square(6, 8, 3, 5);
            Square bottomRight = new Square(6, 8, 6, 8);

            Squares = new List<Square> { topLeft, topMiddle, topRight, middleLeft, center, middleRight, bottomLeft, bottomMiddle, bottomRight };

            populateSquareRows(Squares);
        }

        /// <summary>
        /// Constructs a 9x9 board using an 81 digit string representation of the board.
        /// The representation is constructed by reading a 9x9 board row by row
        /// from left to right starting at the top row and going down. If the cell
        /// has no value, enter 0. Otherwise, enter the 1-9 digit that's in that cell.
        /// </summary>
        /// <param name="boardRepresentation"></param
        /// <remarks>TO DO: Create a more generic representation so that any number of digits can be used instead of only 1-9.</remarks>
        public Board(String boardRepresentation)
            :this()
        {
            setBoardWithRepresentation(boardRepresentation);
        }

        /// <summary>
        /// Constructs a size-by-size board with <paramref name="size"/> rows and <paramref name="size"/> columns using the squares provided.
        /// Each square should have dimensions so that <paramref name="size"/> number of cells are within each square.
        /// The representation is constructed by reading the board row by row
        /// from left to right starting at the top row and going down. If the cell
        /// has no value, enter 0. Otherwise, enter the 1-9 digit that's in that cell.
        /// </summary>
        /// <param name="size">The number of rows and columns on the board.</param>
        /// <param name="squares">The squares within the board. Each square should have dimensions so that <paramref name="size"/> number of cells are within each square.</param>
        /// <param name="boardRepresentation">The board representation.</param>
        public Board(int size, List<Square> squares, String boardRepresentation)
            :this(size, squares)
        {
            setBoardWithRepresentation(boardRepresentation);
        }

        /// <summary>
        /// Constructs a size-by-size board with <paramref name="size"/> rows and <paramref name="size"/> columns using the squares provided.
        /// Each square should have dimensions so that <paramref name="size"/> number of cells are within each square.
        /// </summary>
        /// <param name="size">The number of rows and columns on the board.</param>
        /// <param name="squares">The squares within the board. Each square should have dimensions so that <paramref name="size"/> number of cells are within each square.</param>
        public Board(int size, List<Square> squares)
        {
            List<Row> rows = new List<Row>();
            List<Column> columns = new List<Column>();

            for (int rowNum = 0; rowNum < size; ++rowNum)
            {
                rows.Add(new Row(rowNum, size));
            }

            for (int colNumber = 0; colNumber < size; ++colNumber)
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
            Squares = squares;

            List<CellCollection> allCollections = new List<CellCollection>();
            allCollections.AddRange(Rows);
            allCollections.AddRange(Columns);
            allCollections.AddRange(Squares);

            populateSquareRows(Squares);

            AllCellCollections = allCollections;
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
            Dictionary<int, int> columnMaxPossibilities = new Dictionary<int, int>();

            Debug.WriteLine("VALUES");
            foreach (Row r in Rows)
            {
                foreach (Cell c in r.Cells)
                {
                    String valueStr = c.Value.HasValue ? c.Value.ToString() : "0";
                    valueStr += " ";

                    Debug.Write(valueStr);

                    if (!columnMaxPossibilities.ContainsKey(c.Column))
                    {
                        columnMaxPossibilities[c.Column] = c.Possibilities.Count;
                    }
                    else if (columnMaxPossibilities[c.Column] < c.Possibilities.Count)
                    {
                        columnMaxPossibilities[c.Column] = c.Possibilities.Count;
                    }
                }

                Debug.Write(Environment.NewLine);
            }

            Debug.WriteLine("POSSIBILITIES");
            foreach (Row r in Rows)
            {
                foreach (Cell c in r.Cells)
                {
                    int columnMaxPossibility = columnMaxPossibilities[c.Column];
                    int maxColumnWidth = columnMaxPossibility + (columnMaxPossibility - 1);

                    int paddingNeeded = maxColumnWidth;

                    if (c.Possibilities.Count > 0)
                    {
                        paddingNeeded -= (c.Possibilities.Count + c.Possibilities.Count - 1);
                    }

                    String padding = String.Empty;

                    for (int i = 0; i < paddingNeeded; ++i)
                    {
                        padding += " ";
                    }

                    String possibilitiesStr = "{" + String.Join(",", c.Possibilities.Values) + padding + "}";

                    possibilitiesStr += " ";

                    Debug.Write(possibilitiesStr);
                }

                Debug.Write(Environment.NewLine);
            }
        }

        private void populateSquareRows(IReadOnlyList<Square> squares)
        {
            foreach (Square sq in squares)
            {
                sq.PopulateRows(Rows);
            }
        }

        private void setBoardWithRepresentation(String boardRepresentation)
        {
            if (String.IsNullOrWhiteSpace(boardRepresentation))
            {
                throw new ArgumentNullException("The boardRepresentation is null or empty.");
            }

            int expectedBoardRepresentationLength = Rows.Count * Rows.Count;

            if (boardRepresentation.Length != expectedBoardRepresentationLength)
            {
                throw new ApplicationException("Expected the board representation to have " + expectedBoardRepresentationLength + " characters representing " + expectedBoardRepresentationLength + " cells. There are " + boardRepresentation.Length + " characters.");
            }

            for (int i = 0; i < boardRepresentation.Length; ++i)
            {
                int row = i / Rows.Count;
                int column = i % Rows.Count;

                char valueChar = boardRepresentation[i];

                if (valueChar != '0')
                {
                    int value = int.Parse(valueChar.ToString());
                    setCellValue(row, column, value);
                }
            }
        }
    }
}
