using System;
using System.Collections.Generic;
using Sudoku.Model;
using Sudoku.Solver;

namespace Sudoku
{
    /// <summary>
    /// Given a Sudoku board, this class attempts to solve the board using
    /// the available Sudoku Solvers.
    /// </summary>
    public class SudokuSolver
    {
        private Board _board = null;
        private List<SolverBase> _solvers = new List<SolverBase> {
            new SingleCellPossibilitySolver(),
            new OnlyPossibilityInCellCollectionSolver(),
            new TwoOrThreePossibilitiesSolver(),
            new SquareLineReducerSolver(),
            new ThreePossibilityComboSolver(),
            new FourPossibilityComboSolver()
        };

        /// <summary>
        /// Instantiates the solver with the given Sudoku board.
        /// </summary>
        /// <param name="board">The Sudoku board to solve.</param>
        public SudokuSolver(Board board)
        {
            _board = board;
        }

        /// <summary>
        /// Attempts to solve the Sudoku board using the available Sudoku Solvers.
        /// It stops when the board has been solved or no progress has been made
        /// in reducing the number of possibilities on the board.
        /// </summary>
        public void Solve()
        {
            int remainingPossibilitiesCount = _board.remainingPossibilitiesCount();
            Boolean remainingPossibilitiesReduced = true;

            while (remainingPossibilitiesCount > 0 &&
                   remainingPossibilitiesReduced)
            {
                foreach(SolverBase solver in _solvers)
                {
                    solver.Solve(_board);
                }

                int newRemainingPossibilitiesCount = _board.remainingPossibilitiesCount();
                int numPossibilitiesReduced = remainingPossibilitiesCount - newRemainingPossibilitiesCount;
                remainingPossibilitiesReduced = numPossibilitiesReduced > 0;

                remainingPossibilitiesCount = newRemainingPossibilitiesCount;
            }
        }
    }
}
