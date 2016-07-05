using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Possibilities
    {
        public List<int> Values { get; private set; }

        public int Count
        {
            get
            {
                return Values.Count;
            }
        }

        public void Remove(int possibility)
        {
            Values.Remove(possibility);
        }

        public void RemoveAll(IEnumerable<int> possibilitiesToRemove)
        {
            Values.RemoveAll(v => possibilitiesToRemove.Contains(v));
        }

        public Possibilities()
        {
            Values = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        }
    }
}
