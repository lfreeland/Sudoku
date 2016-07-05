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
                List<PossibilityMatch> matches = findPossibilityMatches(cellCollection.Cells);

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

        protected abstract List<PossibilityMatch> findPossibilityMatches(List<Cell> possibilityCells);

        protected void addMatchIfFound(List<Cell> possibleMatchingCells, int numberOfPossibilities, List<PossibilityMatch> matches)
        {
            HashSet<int> cellsPossibilities = new HashSet<int>();

            foreach (Cell c in possibleMatchingCells)
            {
                cellsPossibilities.UnionWith(c.Possibilities.Values);
            }

            if (cellsPossibilities.Count == numberOfPossibilities &&
                matches.Where(m => m.Possibilities != cellsPossibilities).ToList().Count == 0)
            {
                PossibilityMatch match = new PossibilityMatch();
                match.Cells.AddRange(possibleMatchingCells);
                match.Possibilities = cellsPossibilities;

                matches.Add(match);
            }
        }
    }
}
