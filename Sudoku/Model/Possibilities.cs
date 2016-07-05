using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Model
{
    /// <summary>
    /// The possible values on a cell of a Sudoku board.
    /// </summary>
    public class Possibilities
    {
        /// <summary>
        /// The possible values.
        /// </summary>
        public List<int> Values { get; private set; }

        /// <summary>
        /// The number of possible values.
        /// </summary>
        public int Count
        {
            get
            {
                return Values.Count;
            }
        }

        /// <summary>
        /// Removes the given possibility from the set of possible values.
        /// If the possibility is already removed, the operation does nothing.
        /// </summary>
        /// <param name="possibility">The possibility to remove.</param>
        public void Remove(int possibility)
        {
            Values.Remove(possibility);
        }

        /// <summary>
        /// Removes the given possibilities from the set of possible values.
        /// </summary>
        /// <param name="possibilitiesToRemove">The possibilities to remove.</param>
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
