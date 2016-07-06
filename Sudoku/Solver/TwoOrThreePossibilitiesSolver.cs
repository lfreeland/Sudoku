using System;
using System.Collections.Generic;
using System.Linq;

using Sudoku.Model;

namespace Sudoku.Solver
{
    /// <summary>
    /// This solver identifies two cells or three cells with each cell containing the same
    /// set of possible values in each row, column, and square. If a set of cells is found,
    /// the possibilities are eliminated from the rest of the cells in the row, column or square
    /// the cells were found in.
    /// </summary>
    [Obsolete("Use two GenericPossibilityComboSolver, one with a value of 2 for 'numberOfPossibilities' and the second with a value of 3 for 'numberOfPossibilities'. The GenericPossibilityComboSolver handles these use cases already and there's no need to duplicate code.", true)]
    public class TwoOrThreePossibilitiesSolver : SolverBase
    {
        public override void Solve(Board board)
        {
            foreach (CellCollection cellCollection in board.AllCellCollections)
            {
                foreach (Cell c in cellCollection.Cells)
                {
                    reducePossibilities(c, cellCollection, 2);
                    reducePossibilities(c, cellCollection, 3);
                }
            }
        }

        private void reducePossibilities(Cell currentCell, CellCollection cellCollection, int numberPossibilities)
        {
            if (currentCell.Possibilities.Count != numberPossibilities)
            {
                return;
            }

            List<Cell> otherPosCells = cellCollection.Cells.Where(c2 => c2.Possibilities.Values.SequenceEqual(currentCell.Possibilities.Values)).ToList();

            if (otherPosCells.Count == numberPossibilities)
            {
                List<Cell> samePosCells = new List<Cell>();
                samePosCells.AddRange(otherPosCells);
                samePosCells.Add(currentCell);

                foreach (Cell cellToReduce in cellCollection.Cells.Where(c => samePosCells.Contains(c) == false))
                {
                    cellToReduce.Possibilities.RemoveAll(currentCell.Possibilities.Values);
                }
            }
        }
    }
}
