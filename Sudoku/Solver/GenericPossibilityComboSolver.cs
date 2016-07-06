using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sudoku.Model;

namespace Sudoku.Solver
{
    /// <summary>
    /// Finds N numberOfPossibilities in N cells in each Row, Column, and Square
    /// on the board. For each matching possibilities combination found, remove the possibilities
    /// from the other cells in the Row, Column, or Square the match was found in since the matching
    /// possibilities can't be a possibility in those other cells.
    /// </summary>
    public class GenericPossibilityComboSolver : PossibilityComboReducerBase
    {
        private int _numberOfPossibilities;

        public GenericPossibilityComboSolver(int numberOfPossibilities)
        {
            _numberOfPossibilities = numberOfPossibilities;
        }

        public override List<PossibilityMatch> findPossibilityMatches(List<Cell> possibilityCells)
        {
            List<PossibilityMatch> matches = new List<PossibilityMatch>();

            List<Cell> filteredPossibilityCells = possibilityCells.Where(c => c.Possibilities.Count > 1 && c.Possibilities.Count <= _numberOfPossibilities).ToList();

            if (filteredPossibilityCells.Count < _numberOfPossibilities)
            {
                return matches;
            }

            findPossibilityMatchesRecursive(filteredPossibilityCells, new List<Cell>(), _numberOfPossibilities, 1, matches);

            return matches;
        }

        protected void findPossibilityMatchesRecursive(List<Cell> possibilityCells, List<Cell> traversedCells, int numberOfPossibilities, int currentDepth, List<PossibilityMatch> matches)
        {
            foreach (Cell cell in possibilityCells)
            {
                if (traversedCells.Contains(cell) ||
                    matches.Where(m => m.Cells.Contains(cell)).Any())
                {
                    continue;
                }

                traversedCells.Add(cell);

                if (currentDepth == numberOfPossibilities)
                {
                    addMatchIfFound(traversedCells, numberOfPossibilities, matches);
                }
                else
                {
                    findPossibilityMatchesRecursive(possibilityCells, traversedCells, numberOfPossibilities, currentDepth + 1, matches);
                }

                traversedCells.Remove(cell);
            }
        }
    }
}
