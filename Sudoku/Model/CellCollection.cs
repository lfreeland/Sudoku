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
    }
}
