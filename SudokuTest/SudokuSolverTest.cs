using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku;
using Sudoku.Model;
using System.Collections.Generic;

namespace SudokuTest
{
    /// <summary>
    /// Test class for solving Sudoku puzzles using the SudokuSolver class.
    /// The tests comprise easy, medium, hard, and evil Sudoku boards from
    /// http://www.websudoku.com.
    /// </summary>
    [TestClass]
    public class SudokuSolverTest
    {
        private void solveSudokuBoardTest(String boardRepresentation)
        {
            Board board = new Board(boardRepresentation);

            solveSudokuBoardTest(board);
        }

        private void solve6x6SudokuBoardTest(String sixBySixBoardRepresentation)
        {
            Square topLeft = new Square(0, 1, 0, 2);
            Square topRight = new Square(0, 1, 3, 5);
            Square middleLeft = new Square(2, 3, 0, 2);
            Square middleRight = new Square(2, 3, 3, 5);
            Square bottomLeft = new Square(4, 5, 0, 2);
            Square bottomRight = new Square(4, 5, 3, 5);

            List<Square> squares = new List<Square> { topLeft, topRight, middleLeft, middleRight, bottomLeft, bottomRight };

            solveSudokuBoardTest(squares, sixBySixBoardRepresentation);
        }

        private void solveSudokuBoardTest(List<Square> squares, String boardRepresentation)
        {
            Board board = new Board(squares.Count, squares, boardRepresentation);

            solveSudokuBoardTest(board);
        }

        private void solveSudokuBoardTest(Board board)
        {
            SudokuSolver solver = new SudokuSolver(board);
            solver.Solve();

            board.printDiagnosticsToOutput();

            Assert.IsTrue(board.isSolved());
        }

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

            solveSudokuBoardTest(easyBoard);
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

            String boardReprsentation = "069007005057802090400100673000010300900703002006050000543006001020401530700500240";

            solveSudokuBoardTest(boardReprsentation);
        }

        [TestMethod]
        public void SolveEasyBoardTest3()
        {
            // http://www.websudoku.com/?level=1&set_id=2011249977

            String boardReprsentation = "096000012100729306302000000009080003213000857600050900000000508804516009560000130";

            solveSudokuBoardTest(boardReprsentation);
        }

        [TestMethod]
        public void SolveEasyBoardTest4()
        {
            // http://www.websudoku.com/?level=1&set_id=9792512768

            String boardReprsentation = "005416300310200008069000010070500802601000703802003050090000580400009067006748900";

            solveSudokuBoardTest(boardReprsentation);
        }

        [TestMethod]
        public void SolveMediumBoardTest1()
        {
            // http://www.websudoku.com/?level=2&set_id=6094327244

            String boardReprsentation = "000010900200093570600000800430700000060409030000001046009000005076940002001080000";

            solveSudokuBoardTest(boardReprsentation);
        }

        [TestMethod]
        public void SolveMediumBoardTest2()
        {
            // http://www.websudoku.com/?level=2&set_id=2075924274

            String boardReprsentation = "091000000000008000032079050014700920020060030086001540050930470000400000000000160";

            solveSudokuBoardTest(boardReprsentation);
        }

        [TestMethod]
        public void SolveHardBoardTest1()
        {
            // http://www.websudoku.com/?level=3&set_id=8385086993

            String boardReprsentation = "004000008510607000000004620600100900030000050002009001097500000000908037400000500";

            solveSudokuBoardTest(boardReprsentation);
        }

        [TestMethod]
        public void SolveHardBoardTest2()
        {
            // http://www.websudoku.com/?level=3&set_id=3008976820

            String boardReprsentation = "005008100900000003001640000100000700076529810008000002000012500400000009002800400";

            solveSudokuBoardTest(boardReprsentation);
        }

        [TestMethod]
        public void SolveHardBoardTest3()
        {
            // http://www.websudoku.com/?level=3&set_id=5745978098

            String boardReprsentation = "800560720700001050600000409000080000008205300000010000301000008040100007082039006";

            solveSudokuBoardTest(boardReprsentation);
        }

        [TestMethod]
        public void SolveEvilBoardTest1()
        {
            // http://www.websudoku.com/?level=4&set_id=1304551528

            String boardReprsentation = "750800001000000900004507000030100200601000804005008010000605400002000000400009063";

            solveSudokuBoardTest(boardReprsentation);
        }

        [TestMethod]
        public void SolveEvilBoardTest2()
        {
            // http://www.websudoku.com/?level=4&set_id=1959875216

            String boardReprsentation = "000500006604000000005907000420000805009070200803000079000605700000000301100008000";

            solveSudokuBoardTest(boardReprsentation);
        }

        [TestMethod]
        public void SolveEvilBoardTest3()
        {
            // http://www.websudoku.com/?level=4&set_id=9186368588

            String boardReprsentation = "100000900002049370000200060001007000570000083000800200030004000024530800008000004";

            solveSudokuBoardTest(boardReprsentation);
        }

        [TestMethod]
        public void Solve6x6BoardTest1()
        {
            // http://6x6sudoku.com/ #422023 

            String sixBySixBoardRepresentation = "040030310245053600001450234061060020";

            solve6x6SudokuBoardTest(sixBySixBoardRepresentation);
        }

        [TestMethod]
        public void Solve6x6BoardTest2()
        {
            // http://6x6sudoku.com/ #223050  

            String sixBySixBoardRepresentation = "000000614532300400000000562314100600";

            solve6x6SudokuBoardTest(sixBySixBoardRepresentation);
        }

        [TestMethod]
        public void Solve6x6BoardTest3()
        {
            // http://sudokuplace.com/pdfs/6x6_hard_14.pdf Bottom Right Board

            String sixBySixBoardRepresentation = "020000045023000030060000130250000010";

            solve6x6SudokuBoardTest(sixBySixBoardRepresentation);
        }

        [TestMethod]
        public void Solve6x6BoardTest4()
        {
            // http://sudokuplace.com/pdfs/6x6_hard_14.pdf Bottom Left Board

            String sixBySixBoardRepresentation = "050000020045005200004300340010000020";

            solve6x6SudokuBoardTest(sixBySixBoardRepresentation);
        }

        [TestMethod]
        public void Solve12x12BoardTest1()
        {
            // http://12x12sudoku.com/ #100075

            Square row1Left = new Square(0, 2, 0, 3);
            Square row1Middle = new Square(0, 2, 4, 7);
            Square row1Right = new Square(0, 2, 8, 11);

            Square row2Left = new Square(3, 5, 0, 3);
            Square row2Middle = new Square(3, 5, 4, 7);
            Square row2Right = new Square(3, 5, 8, 11);

            Square row3Left = new Square(6, 8, 0, 3);
            Square row3Middle = new Square(6, 8, 4, 7);
            Square row3Right = new Square(6, 8, 8, 11);

            Square row4Left = new Square(9, 11, 0, 3);
            Square row4Middle = new Square(9, 11, 4, 7);
            Square row4Right = new Square(9, 11, 8, 11);

            List<Square> squares = new List<Square> { row1Left, row1Middle, row1Right, row2Left, row2Middle, row2Right, row3Left, row3Middle, row3Right, row4Left, row4Middle, row4Right };
            Board b = new Board(12, squares);

            b.setCellValue(0, 0, 3);
            b.setCellValue(0, 3, 12);
            b.setCellValue(0, 7, 10);
            b.setCellValue(0, 8, 4);
            b.setCellValue(0, 10, 9);
            b.setCellValue(0, 11, 8);

            b.setCellValue(1, 0, 7);
            b.setCellValue(1, 1, 10);
            b.setCellValue(1, 4, 1);
            b.setCellValue(1, 8, 3);
            b.setCellValue(1, 10, 6);

            b.setCellValue(2, 2, 1);
            b.setCellValue(2, 6, 6);
            b.setCellValue(2, 9, 10);


            b.setCellValue(3, 0, 5);
            b.setCellValue(3, 1, 6);
            b.setCellValue(3, 5, 8);
            b.setCellValue(3, 6, 10);
            b.setCellValue(3, 7, 1);
            b.setCellValue(3, 11, 4);

            b.setCellValue(4, 0, 10);
            b.setCellValue(4, 3, 8);
            b.setCellValue(4, 10, 12);

            b.setCellValue(5, 2, 3);
            b.setCellValue(5, 3, 9);
            b.setCellValue(5, 5, 12);
            b.setCellValue(5, 6, 11);
            b.setCellValue(5, 8, 2);

            b.setCellValue(6, 3, 6);
            b.setCellValue(6, 5, 3);
            b.setCellValue(6, 6, 1);
            b.setCellValue(6, 8, 11);
            b.setCellValue(6, 9, 5);

            b.setCellValue(7, 1, 3);
            b.setCellValue(7, 8, 12);
            b.setCellValue(7, 11, 6);

            b.setCellValue(8, 0, 11);
            b.setCellValue(8, 4, 12);
            b.setCellValue(8, 5, 6);
            b.setCellValue(8, 6, 5);
            b.setCellValue(8, 10, 3);
            b.setCellValue(8, 11, 9);

            b.setCellValue(9, 2, 5);
            b.setCellValue(9, 5, 9);
            b.setCellValue(9, 9, 11);

            b.setCellValue(10, 1, 12);
            b.setCellValue(10, 3, 11);
            b.setCellValue(10, 7, 8);
            b.setCellValue(10, 10, 5);
            b.setCellValue(10, 11, 7);

            b.setCellValue(11, 0, 9);
            b.setCellValue(11, 1, 8);
            b.setCellValue(11, 3, 3);
            b.setCellValue(11, 4, 5);
            b.setCellValue(11, 8, 1);
            b.setCellValue(11, 11, 12);

            solveSudokuBoardTest(b);
        }
    }
}
