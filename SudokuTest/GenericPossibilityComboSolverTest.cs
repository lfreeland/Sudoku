using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku.Model;
using Sudoku.Solver;
using System.Linq;
using System.Collections.Generic;

namespace SudokuTest
{
    /// <summary>
    /// Test class that contains test cases for the GenericPossibilityComboSolver.
    /// </summary>
    [TestClass]
    public class GenericPossibilityComboSolverTest
    {
        [TestMethod]
        public void findTwoIdenticalPossibilitiesInTwoCellsTest()
        {
            Row row = new Row();
            for (int column = 0; column <= 6; ++column)
            {
                row.Cells[column].Possibilities.Values.Clear();
            }

            row.Cells[7].Possibilities.RemoveAllExcept(new List<int> { 2, 3 });
            row.Cells[8].Possibilities.RemoveAllExcept(new List<int> { 2, 3 });

            Assert.AreEqual(2, row.Cells[7].Possibilities.Count, "The cell in column 7 does not have two possibilities.");
            Assert.AreEqual(2, row.Cells[8].Possibilities.Count, "The cell in column 8 does not have two possibilities.");

            GenericPossibilityComboSolver gpcs = new GenericPossibilityComboSolver(2);
            List<PossibilityMatch> matches = gpcs.findPossibilityMatches(row.Cells);

            Assert.AreEqual(1, matches.Count, "One match expected.");

            PossibilityMatch match = matches[0];

            Assert.IsTrue(match.Cells.Contains(row.Cells[7]), "The column 7 cell was not in the match.");
            Assert.IsTrue(match.Cells.Contains(row.Cells[8]), "The column 8 cell was not in the match.");

            foreach (int expectedPossibility in row.Cells[7].Possibilities.Values)
            {
                Assert.IsTrue(match.Possibilities.Contains(expectedPossibility), "The match does not have an expected possibility of " + expectedPossibility);
            }
        }

        [TestMethod]
        public void findThreeIdenticalPossibilitiesInThreeCellsTest()
        {
            Row row = new Row();
            for (int column = 0; column <= 5; ++column)
            {
                row.Cells[column].Possibilities.Values.Clear();
            }

            row.Cells[6].Possibilities.RemoveAllExcept(new List<int> { 1, 2, 3 });
            row.Cells[7].Possibilities.RemoveAllExcept(new List<int> { 1, 2, 3 });
            row.Cells[8].Possibilities.RemoveAllExcept(new List<int> { 1, 2, 3 });

            Assert.AreEqual(3, row.Cells[7].Possibilities.Count, "The cell in column 6 does not have three possibilities.");
            Assert.AreEqual(3, row.Cells[7].Possibilities.Count, "The cell in column 7 does not have three possibilities.");
            Assert.AreEqual(3, row.Cells[8].Possibilities.Count, "The cell in column 8 does not have three possibilities.");

            GenericPossibilityComboSolver gpcs = new GenericPossibilityComboSolver(3);
            List<PossibilityMatch> matches = gpcs.findPossibilityMatches(row.Cells);

            Assert.AreEqual(1, matches.Count, "One match expected.");

            PossibilityMatch match = matches[0];

            Assert.IsTrue(match.Cells.Contains(row.Cells[6]), "The column 6 cell was not in the match.");
            Assert.IsTrue(match.Cells.Contains(row.Cells[7]), "The column 7 cell was not in the match.");
            Assert.IsTrue(match.Cells.Contains(row.Cells[8]), "The column 8 cell was not in the match.");

            foreach (int expectedPossibility in row.Cells[7].Possibilities.Values)
            {
                Assert.IsTrue(match.Possibilities.Contains(expectedPossibility), "The match does not have an expected possibility of " + expectedPossibility);
            }
        }

        [TestMethod]
        public void findTwoMatchesFromTwoIdenticalPossibilitiesInFourCellsTest()
        {
            Row row = new Row();
            for (int column = 0; column <= 4; ++column)
            {
                row.Cells[column].Possibilities.Values.Clear();
            }

            List<int> firstPossibilitiesToRemove = Enumerable.Range(1, 7).ToList();

            row.Cells[7].Possibilities.RemoveAllExcept(new List<int> { 8, 9 });
            row.Cells[8].Possibilities.RemoveAllExcept(new List<int> { 8, 9 });

            Assert.AreEqual(2, row.Cells[7].Possibilities.Count, "The cell in column 7 does not have two possibilities.");
            Assert.AreEqual(2, row.Cells[8].Possibilities.Count, "The cell in column 8 does not have two possibilities.");

            List<int> secondPossibilitiesToRemove = Enumerable.Range(3, 7).ToList();

            row.Cells[5].Possibilities.RemoveAllExcept(new List<int> { 1, 2 });
            row.Cells[6].Possibilities.RemoveAllExcept(new List<int> { 1, 2 });

            Assert.AreEqual(2, row.Cells[5].Possibilities.Count, "The cell in column 5 does not have two possibilities.");
            Assert.AreEqual(2, row.Cells[6].Possibilities.Count, "The cell in column 6 does not have two possibilities.");

            GenericPossibilityComboSolver gpcs = new GenericPossibilityComboSolver(2);
            List<PossibilityMatch> matches = gpcs.findPossibilityMatches(row.Cells);

            Assert.AreEqual(2, matches.Count, "Two matches expected.");

            PossibilityMatch match = matches[0];

            Assert.IsTrue(match.Cells.Contains(row.Cells[5]), "The column 5 cell was not in the match.");
            Assert.IsTrue(match.Cells.Contains(row.Cells[6]), "The column 6 cell was not in the match.");

            foreach (int expectedPossibility in row.Cells[5].Possibilities.Values)
            {
                Assert.IsTrue(match.Possibilities.Contains(expectedPossibility), "The match does not have an expected possibility of " + expectedPossibility);
            }

            PossibilityMatch secondMatch = matches[1];

            Assert.IsTrue(secondMatch.Cells.Contains(row.Cells[7]), "The column 7 cell was not in the match.");
            Assert.IsTrue(secondMatch.Cells.Contains(row.Cells[8]), "The column 8 cell was not in the match.");

            foreach (int expectedPossibility in row.Cells[7].Possibilities.Values)
            {
                Assert.IsTrue(secondMatch.Possibilities.Contains(expectedPossibility), "The second match does not have an expected possibility of " + expectedPossibility);
            }
        }

        [TestMethod]
        public void findMatchesFromThreeCellsWithThePossibilityPattern_AB_BC_ACTest()
        {
            Row row = new Row();
            for (int column = 0; column <= 5; ++column)
            {
                row.Cells[column].Possibilities.Values.Clear();
            }

            // Use possibilities 7,8,9 in the {a,b}, {b,c}, {a,c} pattern.

            row.Cells[6].Possibilities.RemoveAllExcept(new List<int> { 7, 8 });
            row.Cells[7].Possibilities.RemoveAllExcept(new List<int> { 8, 9 });
            row.Cells[8].Possibilities.RemoveAllExcept(new List<int> { 7, 9 });

            Assert.AreEqual(2, row.Cells[6].Possibilities.Count, "The cell in column 6 does not have two possibilities.");
            Assert.AreEqual(2, row.Cells[7].Possibilities.Count, "The cell in column 7 does not have two possibilities.");
            Assert.AreEqual(2, row.Cells[8].Possibilities.Count, "The cell in column 8 does not have two possibilities.");

            GenericPossibilityComboSolver gpcs = new GenericPossibilityComboSolver(3);
            List<PossibilityMatch> matches = gpcs.findPossibilityMatches(row.Cells);

            Assert.AreEqual(1, matches.Count, "One match expected.");

            PossibilityMatch match = matches[0];

            Assert.IsTrue(match.Cells.Contains(row.Cells[6]), "The column 6 cell was not in the match.");
            Assert.IsTrue(match.Cells.Contains(row.Cells[7]), "The column 7 cell was not in the match.");
            Assert.IsTrue(match.Cells.Contains(row.Cells[8]), "The column 8 cell was not in the match.");

            List<int> expectedPossibilities = new List<int> { 7, 8, 9 };

            foreach (int expectedPossibility in expectedPossibilities)
            {
                Assert.IsTrue(match.Possibilities.Contains(expectedPossibility), "The match does not have an expected possibility of " + expectedPossibility);
            }

        }

        [TestMethod]
        public void findMatchesFromThreeCellsWithThePossibilityPattern_AB_BC_ABCTest()
        {
            Row row = new Row();
            for (int column = 0; column <= 5; ++column)
            {
                row.Cells[column].Possibilities.Values.Clear();
            }

            // Use possibilities 7,8,9 in the {a,b}, {b,c}, {a,b,c} pattern.

            row.Cells[6].Possibilities.RemoveAllExcept(new List<int> { 7, 8 });
            row.Cells[7].Possibilities.RemoveAllExcept(new List<int> { 8, 9 });
            row.Cells[8].Possibilities.RemoveAllExcept(new List<int> { 7, 8, 9 });

            Assert.AreEqual(2, row.Cells[6].Possibilities.Count, "The cell in column 6 does not have two possibilities.");
            Assert.AreEqual(2, row.Cells[7].Possibilities.Count, "The cell in column 7 does not have two possibilities.");
            Assert.AreEqual(3, row.Cells[8].Possibilities.Count, "The cell in column 8 does not have three possibilities.");

            GenericPossibilityComboSolver gpcs = new GenericPossibilityComboSolver(3);
            List<PossibilityMatch> matches = gpcs.findPossibilityMatches(row.Cells);

            Assert.AreEqual(1, matches.Count, "One match expected.");

            PossibilityMatch match = matches[0];

            Assert.IsTrue(match.Cells.Contains(row.Cells[6]), "The column 6 cell was not in the match.");
            Assert.IsTrue(match.Cells.Contains(row.Cells[7]), "The column 7 cell was not in the match.");
            Assert.IsTrue(match.Cells.Contains(row.Cells[8]), "The column 8 cell was not in the match.");

            List<int> expectedPossibilities = new List<int> { 7, 8, 9 };

            foreach (int expectedPossibility in expectedPossibilities)
            {
                Assert.IsTrue(match.Possibilities.Contains(expectedPossibility), "The match does not have an expected possibility of " + expectedPossibility);
            }
        }

        [TestMethod]
        public void findMatchesFromThreeCellsWithThePossibilityPattern_AB_ABC_ABCTest()
        {
            Row row = new Row();
            for (int column = 0; column <= 5; ++column)
            {
                row.Cells[column].Possibilities.Values.Clear();
            }

            // Use possibilities 7,8,9 in the {a,b}, {b,c}, {a,b,c} pattern.

            row.Cells[6].Possibilities.RemoveAllExcept(new List<int> { 7, 8 });
            row.Cells[7].Possibilities.RemoveAllExcept(new List<int> { 7, 8, 9 });
            row.Cells[8].Possibilities.RemoveAllExcept(new List<int> { 7, 8, 9 });

            Assert.AreEqual(2, row.Cells[6].Possibilities.Count, "The cell in column 6 does not have two possibilities.");
            Assert.AreEqual(3, row.Cells[7].Possibilities.Count, "The cell in column 7 does not have three possibilities.");
            Assert.AreEqual(3, row.Cells[8].Possibilities.Count, "The cell in column 8 does not have three possibilities.");

            GenericPossibilityComboSolver gpcs = new GenericPossibilityComboSolver(3);
            List<PossibilityMatch> matches = gpcs.findPossibilityMatches(row.Cells);

            Assert.AreEqual(1, matches.Count, "One match expected.");

            PossibilityMatch match = matches[0];

            Assert.IsTrue(match.Cells.Contains(row.Cells[6]), "The column 6 cell was not in the match.");
            Assert.IsTrue(match.Cells.Contains(row.Cells[7]), "The column 7 cell was not in the match.");
            Assert.IsTrue(match.Cells.Contains(row.Cells[8]), "The column 8 cell was not in the match.");

            List<int> expectedPossibilities = new List<int> { 7, 8, 9 };

            foreach (int expectedPossibility in expectedPossibilities)
            {
                Assert.IsTrue(match.Possibilities.Contains(expectedPossibility), "The match does not have an expected possibility of " + expectedPossibility);
            }
        }
    }
}
