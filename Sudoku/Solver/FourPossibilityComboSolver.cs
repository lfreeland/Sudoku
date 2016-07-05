using System.Collections.Generic;
using System.Linq;

using Sudoku.Model;

namespace Sudoku.Solver
{
    /// <summary>
    /// Find 4 cells in a cell collection (row, column or square)
    /// that have a combination of four possible values.
    /// If combination found, eliminate the four possibilities
    /// from the rest of the cell collection since those possibilities
    /// can only be in those 4 cells.
    /// </summary>
    public class FourPossibilityComboSolver : PossibilityComboReducerBase
    {
        protected override List<PossibilityMatch> findPossibilityMatches(List<Cell> possibilityCells)
        {
            List<PossibilityMatch> matches = new List<PossibilityMatch>();

            List<Cell> twoOrThreeOrFourPossibilityCells = possibilityCells.Where(c => c.Possibilities.Count >= 2 && c.Possibilities.Count <= 4).ToList();

            if (twoOrThreeOrFourPossibilityCells.Count < 4)
            {
                return matches;
            }

            foreach (Cell firstCell in twoOrThreeOrFourPossibilityCells)
            {
                foreach (Cell secondCell in twoOrThreeOrFourPossibilityCells)
                {
                    if (secondCell == firstCell)
                    {
                        continue;
                    }

                    foreach (Cell thirdCell in twoOrThreeOrFourPossibilityCells)
                    {
                        if (thirdCell == firstCell ||
                            thirdCell == secondCell)
                        {
                            continue;
                        }

                        foreach (Cell fourthCell in twoOrThreeOrFourPossibilityCells)
                        {
                            if (fourthCell == firstCell ||
                                fourthCell == secondCell ||
                                fourthCell == thirdCell)
                            {
                                continue;
                            }

                            base.addMatchIfFound(new List<Cell> { firstCell, secondCell, thirdCell, fourthCell }, 4, matches);
                        }
                    }
                }
            }

            return matches;
        }
    }
}
