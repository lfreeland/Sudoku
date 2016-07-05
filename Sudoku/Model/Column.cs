using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Model
{
    /// <summary>
    /// A column of cells on a Sudoku board.
    /// </summary>
    /// <remarks>The cells are not instantiated because they are shared with other cells from other collections.</remarks>
    public class Column : CellCollection
    {
        public Column()
        {
            
        }
    }
}