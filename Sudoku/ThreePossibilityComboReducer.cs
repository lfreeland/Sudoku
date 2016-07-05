using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class ThreePossibilityComboReducer
    {
        private Board _board;

        public ThreePossibilityComboReducer(Board board)
        {
            _board = board;
        }

        private class PossibilityMatch
        {
            public List<Cell> Cells { get; set; }

            public HashSet<int> Possibilities { get; set; }

            public PossibilityMatch()
            {
                Cells = new List<Cell>();
            }
        }

        public void Apply()
        {
            // Find 3 cells in a cell collection (row, column or square)
            // that have the combination {a,b}, {b,c}, {a,c} OR
            // {a,b}, {b,c}, {a,b,c} OR {a,b}, {a,b,c}, {a,b,c}.
            // If combination found, eliminate the a,b,c possibilities
            // from the rest of the cell collection since those possibilities
            // can only be in those 3 cells.

            foreach (CellCollection cellCollection in _board.AllCellCollections)
            {
                List<Cell> twoOrThreePossibilityCells = cellCollection.Cells.Where(c => c.Possibilities.Count == 2 || c.Possibilities.Count == 3).ToList();

                if (twoOrThreePossibilityCells.Count < 3)
                {
                    continue;
                }

                List<PossibilityMatch> matches = find3PossibilityMatches(twoOrThreePossibilityCells);

                foreach(PossibilityMatch match in matches)
                {
                    foreach(Cell cell in cellCollection.Cells)
                    {
                        if (match.Cells.Contains(cell) == false)
                        {
                            cell.Possibilities.RemoveAll(match.Possibilities);
                        }
                    }
                }
            }
        }

        private List<PossibilityMatch> find3PossibilityMatches(List<Cell> twoOrThreePossibilityCells)
        {
            List<PossibilityMatch> matches = new List<PossibilityMatch>();

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

                        HashSet<int> threeCellPossibilities = new HashSet<int>();

                        threeCellPossibilities.UnionWith(firstTwoOrThreePoscell.Possibilities.Values);
                        threeCellPossibilities.UnionWith(secondTwoOrThreePosCell.Possibilities.Values);
                        threeCellPossibilities.UnionWith(thirdTwoOrThreePosCell.Possibilities.Values);

                        if (threeCellPossibilities.Count == 3 &&
                            matches.Where(m => m.Possibilities != threeCellPossibilities).ToList().Count == 0)
                        {
                            PossibilityMatch match = new PossibilityMatch();
                            match.Cells.Add(firstTwoOrThreePoscell);
                            match.Cells.Add(secondTwoOrThreePosCell);
                            match.Cells.Add(thirdTwoOrThreePosCell);

                            match.Possibilities = threeCellPossibilities;

                            matches.Add(match);
                        }
                    }
                }
            }

            return matches;
        }
    }
}