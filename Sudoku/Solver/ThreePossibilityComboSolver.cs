using System.Collections.Generic;
using System.Linq;

using Sudoku.Model;

namespace Sudoku.Solver
{
    /// <summary>
    /// Find 3 cells in a cell collection (row, column or square)
    /// that have the combination {a,b}, {b,c}, {a,c} OR
    /// {a,b}, {b,c}, {a,b,c} OR {a,b}, {a,b,c}, {a,b,c}.
    /// If combination found, eliminate the a,b,c possibilities
    /// from the rest of the cell collection since those possibilities
    /// can only be in those 3 cells.
    /// </summary>
    public class ThreePossibilityComboSolver : PossibilityComboReducerBase
    {
        protected override List<PossibilityMatch> findPossibilityMatches(List<Cell> possibilityCells)
        {
            List<PossibilityMatch> matches = new List<PossibilityMatch>();
            List<Cell> twoOrThreePossibilityCells = possibilityCells.Where(c => c.Possibilities.Count == 2 || c.Possibilities.Count == 3).ToList();

            if (twoOrThreePossibilityCells.Count < 3)
            {
                return matches;
            }

            foreach (Cell firstTwoOrThreePoscell in twoOrThreePossibilityCells)
            {
                foreach (Cell secondTwoOrThreePosCell in twoOrThreePossibilityCells)
                {
                    if (secondTwoOrThreePosCell == firstTwoOrThreePoscell)
                    {
                        continue;
                    }

                    foreach (Cell thirdTwoOrThreePosCell in twoOrThreePossibilityCells)
                    {
                        if (thirdTwoOrThreePosCell == firstTwoOrThreePoscell ||
                            thirdTwoOrThreePosCell == secondTwoOrThreePosCell)
                        {
                            continue;
                        }

                        base.addMatchIfFound(new List<Cell> { firstTwoOrThreePoscell, secondTwoOrThreePosCell, thirdTwoOrThreePosCell }, 3, matches);
                    }
                }
            }

            return matches;
        }
    }
}