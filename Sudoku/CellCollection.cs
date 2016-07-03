using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class CellCollection
    {
        public List<Cell> Cells { get; set; }

        public void RemovePossibility(int possibility)
        {
            foreach (Cell cell in Cells)
            {
                cell.Possibilities.Remove(possibility);
            }
        }
    }
}
