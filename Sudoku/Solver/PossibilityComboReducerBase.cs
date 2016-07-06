using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sudoku.Model;

namespace Sudoku.Solver
{
    /// <summary>
    /// Base class for a Possibility Combo Reducer Solver. Descendant classes
    /// are responsible for identifying Possibility Matches and this class
    /// will remove the matching possibilities from the remaining cells in the row,
    /// column, or square that the match was found in.
    /// </summary>
    public abstract class PossibilityComboReducerBase : SolverBase
    {
        public override void Solve(Board board)
        {
            foreach (CellCollection cellCollection in board.AllCellCollections)
            {
                List<PossibilityMatch> matches = findPossibilityMatches(cellCollection.UnsolvedCells.ToList());

                foreach (PossibilityMatch match in matches)
                {
                    foreach (Cell cell in cellCollection.Cells)
                    {
                        if (match.Cells.Contains(cell) == false)
                        {
                            cell.Possibilities.RemoveAll(match.Possibilities);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Finds the possibility matches in the given set of possibilityCells.
        /// </summary>
        /// <param name="possibilityCells">The cells to search for possibility combination matches.</param>
        /// <returns>The list of possibility matches.</returns>
        public abstract List<PossibilityMatch> findPossibilityMatches(List<Cell> possibilityCells);

        protected void addMatchIfFound(List<Cell> possibleMatchingCells, int numberOfPossibilities, List<PossibilityMatch> matches)
        {
            SortedSet<int> cellsPossibilities = new SortedSet<int>();

            foreach (Cell c in possibleMatchingCells)
            {
                cellsPossibilities.UnionWith(c.Possibilities.Values);
            }

            if (cellsPossibilities.Count == numberOfPossibilities &&
                matches.Where(m => m.Possibilities.SequenceEqual(cellsPossibilities)).ToList().Count == 0)
            {
                PossibilityMatch match = new PossibilityMatch();
                match.Cells.AddRange(possibleMatchingCells);
                match.Possibilities = cellsPossibilities;

                matches.Add(match);
            }
        }
    }
}
