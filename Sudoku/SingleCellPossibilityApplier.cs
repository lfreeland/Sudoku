using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class SingleCellPossibilityApplier
    {
        private Board _board;

        public SingleCellPossibilityApplier(Board board)
        {
            _board = board;
        }

        public void Apply()
        {
            foreach (Row row in _board.Rows)
            {
                foreach (Cell cell in row.Cells)
                {
                    if (cell.Value.HasValue == false &&
                        cell.Possibilities.Count == 1)
                    {
                        _board.setCellValue(cell, cell.Possibilities.Values[0]);
                    }
                }
            }
        }
    }
}
