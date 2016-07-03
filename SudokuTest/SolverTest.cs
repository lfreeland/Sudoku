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
            easyBoard.setCellValue(1, 0, 5);
            easyBoard.setCellValue(1, 1, 5);
            easyBoard.setCellValue(1, 8, 6);
            easyBoard.setCellValue(0, 0, 2);
            easyBoard.setCellValue(0, 0, 2);
            easyBoard.setCellValue(0, 0, 2);
            easyBoard.setCellValue(0, 0, 2);
            easyBoard.setCellValue(0, 0, 2);
            easyBoard.setCellValue(0, 0, 2);
            easyBoard.setCellValue(0, 0, 2);
            easyBoard.setCellValue(0, 0, 2);
            easyBoard.setCellValue(0, 0, 2);
            easyBoard.setCellValue(0, 0, 2);
        }
    }
}
