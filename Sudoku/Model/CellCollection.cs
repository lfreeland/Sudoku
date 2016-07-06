using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Model
{
    /// <summary>
    /// A collection of cells that can be used in a Row, Column, or Square.
    /// </summary>
    public class CellCollection
    {
        /// <summary>
        /// The cells in the collection.
        /// </summary>
        public List<Cell> Cells { get; set; }

        public IEnumerable<Cell> UnsolvedCells
        {
            get
            {
                return Cells.Where(c => c.Value.HasValue == false);
            }
        }

        /// <summary>
        /// The possibilities in this collection.
        /// </summary>
        public Possibilities Possibilities
        {
            get
            {
                HashSet<int> possibilities = new HashSet<int>();

                foreach (Cell c in UnsolvedCells)
                {
                    foreach (int possibility in c.Possibilities.Values)
                    {
                        possibilities.Add(possibility);
                    }
                }

                Possibilities cellPossibilities = new Possibilities();
                cellPossibilities.Values.Clear();
                cellPossibilities.Values.AddRange(possibilities);

                return cellPossibilities;
            }
        }

        public CellCollection()
        {
            Cells = new List<Cell>();
        }

        /// <summary>
        /// Removes the given possibility from all the cells' possibilities.
        /// </summary>
        /// <param name="possibility"></param>
        public void RemovePossibility(int possibility)
        {
            foreach (Cell cell in Cells)
            {
                cell.Possibilities.Remove(possibility);
            }
        }

        /// <summary>
        /// Removes the given possibility from all the cells not excluded from removal.
        /// </summary>
        /// <param name="possibility">The possibility to remove.</param>
        /// <param name="cellsToExcludeRemoval">The cells to leave the possibility in.</param>
        public void RemovePossibility(int possibility, List<Cell> cellsToExcludeRemoval)
        {
            foreach (Cell cell in Cells)
            {
                if (cellsToExcludeRemoval.Contains(cell) == false)
                {
                    cell.Possibilities.Remove(possibility);
                }
            }
        }

        /// <summary>
        /// Gets the cells that contain the given possibility.
        /// </summary>
        /// <param name="possibility">The possibility to search for in the cells.</param>
        /// <returns>The list of cells that contain the possibility, if any.</returns>
        public List<Cell> getCellsWithPossibility(int possibility)
        {
            return Cells.Where(c => c.Possibilities.Values.Contains(possibility)).ToList();
        }
    }
}
