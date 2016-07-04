using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class TwoOrThreePossibilitiesReducer
    {
        private Board _board;

        public TwoOrThreePossibilitiesReducer(Board board)
        {
            _board = board;
        }

        public void Apply()
        {
            foreach (CellCollection cellCollection in _board.AllCellCollections)
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
