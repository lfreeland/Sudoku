using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// Populates the possibilities with 1 through 9.
        /// </summary>
        public Possibilities()
            : this(9)
        {
        }

        /// <summary>
        /// Populates the possibilities with 1 through numPossibilities.
        /// </summary>
        /// <param name="numPossibilities">The number of possibilities</param>
        public Possibilities(int numPossibilities)
        {
            Values = Enumerable.Range(1, numPossibilities).ToList();
        }

        /// <summary>
        /// Removes all possibilities except the specified possibilities to keep.
        /// </summary>
        /// <param name="possibilitiesToKeep">The possibilities to keep.</param>
        public void RemoveAllExcept(IEnumerable<int> possibilitiesToKeep)
        {
            Values.RemoveAll(v => possibilitiesToKeep.Contains(v) == false);
        }
    }
}
