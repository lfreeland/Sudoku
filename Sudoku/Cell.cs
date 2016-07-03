using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Cell
    {
        public Possibilities Possibilities { get; private set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public Square Square { get; set; }

        private int? val = null;
        public int? Value
        {
            get
            {
                return val;
            }
            set
            {
                val = value;

                Possibilities.Values.Clear();
            }
        }

        public Cell()
        {
            Possibilities = new Possibilities();
        }
    }
}
