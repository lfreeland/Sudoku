using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku;

namespace SudokuTest
{
    [TestClass]
    public class SolverTest
    {
        [TestMethod]
        public void SolveEasyBoardTest()
        {
            // http://www.websudoku.com/?level=1&set_id=10306096203
            /* 200710080
               150000006
               860004071
               008090010
               600070008
               070040500
               780400063
               300000024
               020083009
            */

            Board easyBoard = new Board();
            easyBoard.setCellValue(0, 0, 2);
            easyBoard.setCellValue(0, 3, 7);
            easyBoard.setCellValue(0, 4, 1);
            easyBoard.setCellValue(0, 7, 8);
            easyBoard.setCellValue(1, 0, 1);
            easyBoard.setCellValue(1, 1, 5);
            easyBoard.setCellValue(1, 8, 6);
            easyBoard.setCellValue(2, 0, 8);
            easyBoard.setCellValue(2, 1, 6);
            easyBoard.setCellValue(2, 5, 4);
            easyBoard.setCellValue(2, 7, 7);
            easyBoard.setCellValue(2, 8, 1);
            easyBoard.setCellValue(3, 2, 8);
            easyBoard.setCellValue(3, 4, 9);
            easyBoard.setCellValue(3, 7, 1);
            easyBoard.setCellValue(4, 0, 6);
            easyBoard.setCellValue(4, 4, 7);
            easyBoard.setCellValue(4, 8, 8);
            easyBoard.setCellValue(5, 1, 7);
            easyBoard.setCellValue(5, 4, 4);
            easyBoard.setCellValue(5, 6, 5);
            easyBoard.setCellValue(6, 0, 7);
            easyBoard.setCellValue(6, 1, 8);
            easyBoard.setCellValue(6, 3, 4);
            easyBoard.setCellValue(6, 7, 6);
            easyBoard.setCellValue(6, 8, 3);
            easyBoard.setCellValue(7, 0, 3);
            easyBoard.setCellValue(7, 7, 2);
            easyBoard.setCellValue(7, 8, 4);
            easyBoard.setCellValue(8, 1, 2);
            easyBoard.setCellValue(8, 4, 8);
            easyBoard.setCellValue(8, 5, 3);
            easyBoard.setCellValue(8, 8, 9);

            Solver solver = new Solver(easyBoard);
            solver.Solve();

            easyBoard.printValuesToOutput();

            Assert.IsTrue(easyBoard.isSolved());
        }

        [TestMethod]
        public void SolveEasyBoardTest2()
        {
            // http://www.websudoku.com/?level=1&set_id=10306096203
            /* 069007005
               057802090
               400100673
               000010300
               900703002
               006050000
               543006001
               020401530
               700500240
            */

            String boardString = "069007005057802090400100673000010300900703002006050000543006001020401530700500240";

            Board easyBoard = new Board(boardString);

            Solver solver = new Solver(easyBoard);
            solver.Solve();

            easyBoard.printValuesToOutput();

            int remainingCellsToSolve = easyBoard.remainingCellsToSolveCount();
            Assert.IsTrue(easyBoard.isSolved());
        }

        [TestMethod]
        public void SolveEasyBoardTest3()
        {
            // http://www.websudoku.com/?level=1&set_id=2011249977

            String boardString = "096000012100729306302000000009080003213000857600050900000000508804516009560000130";

            Board easyBoard = new Board(boardString);

            Solver solver = new Solver(easyBoard);
            solver.Solve();

            Assert.IsTrue(easyBoard.isSolved());
        }

        [TestMethod]
        public void SolveEasyBoardTest4()
        {
            // http://www.websudoku.com/?level=1&set_id=9792512768

            String boardString = "005416300310200008069000010070500802601000703802003050090000580400009067006748900";

            Board easyBoard = new Board(boardString);

            Solver solver = new Solver(easyBoard);
            solver.Solve();

            Assert.IsTrue(easyBoard.isSolved());
        }

        [TestMethod]
        public void SolveMediumBoardTest1()
        {
            // http://www.websudoku.com/?level=2&set_id=6094327244

            String boardString = "000010900200093570600000800430700000060409030000001046009000005076940002001080000";

            Board mediumBoard = new Board(boardString);

            Solver solver = new Solver(mediumBoard);
            solver.Solve();

            Assert.IsTrue(mediumBoard.isSolved());
        }

        [TestMethod]
        public void SolveMediumBoardTest2()
        {
            // http://www.websudoku.com/?level=2&set_id=2075924274

            String boardString = "091000000000008000032079050014700920020060030086001540050930470000400000000000160";

            Board mediumBoard = new Board(boardString);

            Solver solver = new Solver(mediumBoard);
            solver.Solve();

            Assert.IsTrue(mediumBoard.isSolved());
        }

        [TestMethod]
        public void SolveHardBoardTest1()
        {
            // http://www.websudoku.com/?level=3&set_id=8385086993

            String boardString = "004000008510607000000004620600100900030000050002009001097500000000908037400000500";

            Board hardBoard = new Board(boardString);

            Solver solver = new Solver(hardBoard);
            solver.Solve();

            Assert.IsTrue(hardBoard.isSolved());
        }

        [TestMethod]
        public void SolveHardBoardTest2()
        {
            // http://www.websudoku.com/?level=3&set_id=3008976820

            String boardString = "005008100900000003001640000100000700076529810008000002000012500400000009002800400";

            Board hardBoard = new Board(boardString);

            Solver solver = new Solver(hardBoard);
            solver.Solve();

            Assert.IsTrue(hardBoard.isSolved());
        }

        [TestMethod]
        public void SolveHardBoardTest3()
        {
            // http://www.websudoku.com/?level=3&set_id=5745978098

            String boardString = "800560720700001050600000409000080000008205300000010000301000008040100007082039006";

            Board hardBoard = new Board(boardString);

            Solver solver = new Solver(hardBoard);
            solver.Solve();

            Assert.IsTrue(hardBoard.isSolved());
        }

        [TestMethod]
        public void SolveEvilBoardTest1()
        {
            // http://www.websudoku.com/?level=4&set_id=1304551528

            String boardString = "750800001000000900004507000030100200601000804005008010000605400002000000400009063";

            Board evilBoard = new Board(boardString);

            Solver solver = new Solver(evilBoard);
            solver.Solve();

            evilBoard.printValuesToOutput();

            Assert.IsTrue(evilBoard.isSolved());
        }
    }
}
