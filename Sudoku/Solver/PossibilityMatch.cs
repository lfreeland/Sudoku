using System.Collections.Generic;

using Sudoku.Model;

namespace Sudoku.Solver
{
    /// <summary>
    /// Represents the match of possible values across an N-Combination of cells.
    /// Used by the Possibility Combo Solvers.
    /// </summary>
    public class PossibilityMatch
    {
        /// <summary>
        /// The cells containing the N-Combination of values.
        /// </summary>
        public List<Cell> Cells { get; set; }

        /// <summary>
        /// The N possible values in the match.
        /// </summary>
        public HashSet<int> Possibilities { get; set; }

        public PossibilityMatch()
        {
            Cells = new List<Cell>();
        }
    }
}
