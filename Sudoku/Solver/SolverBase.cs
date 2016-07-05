using Sudoku.Model;

namespace Sudoku.Solver
{
    /// <summary>
    /// Represents the base class of a Sudoku solver.
    /// </summary>
    /// <remarks>Considered using an interface but thought that using a base class
    /// would be more flexible for future changes.
    /// </remarks>
    public abstract class SolverBase
    {
        /// <summary>
        /// Reduces the possibilities on the Sudoku board or sets the
        /// values in the cells of the Sudoku board.
        /// </summary>
        /// <param name="board">The Sudoku board to solve</param>
        public abstract void Solve(Board board);
    }
}
