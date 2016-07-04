using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Solver
    {
        private Board _board = null;

        public Solver(Board board)
        {
            _board = board;
        }

        public void Solve()
        {
            SingleCellPossibilityApplier scpa = new SingleCellPossibilityApplier(_board);
            OnlyPossibilityInCellCollectionApplier opicca = new OnlyPossibilityInCellCollectionApplier(_board);
            TwoOrThreePossibilitiesReducer totpr = new TwoOrThreePossibilitiesReducer(_board);
            SquareLineReducer slr = new SquareLineReducer(_board);

            int remainingPossibilitiesCount = _board.remainingPossibilitiesCount();
            Boolean remainingPossibilitiesReduced = true;

            while (remainingPossibilitiesCount > 0 &&
                   remainingPossibilitiesReduced)
            {
                scpa.Apply();
                opicca.Apply();
                totpr.Apply();
                slr.Apply();

                int newRemainingPossibilitiesCount = _board.remainingPossibilitiesCount();
                int numPossibilitiesReduced = remainingPossibilitiesCount - newRemainingPossibilitiesCount;
                remainingPossibilitiesCount = newRemainingPossibilitiesCount;

                remainingPossibilitiesReduced = numPossibilitiesReduced > 0;
            }
        }
    }
}
