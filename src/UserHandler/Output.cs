using Sudoku.BoardBuilding;
using Sudoku.BoardSolving;
using Sudoku.Validation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.UserHandler
{
    public static class Output
    {
        public static void PresentBoardToUser(Solve solve)
        {
            solve.Board.PrintBoard();
        }
        public static void PresentUnsolvableToUser()
        {
            Console.WriteLine("the board is unsolvable");
        }
        public static string PresentBoardForTests(Solve solve)
        {
            return solve.Board.GetBoardAsString();
        }
        public static string PresentUsolvableBoardForTests()
        {
            return "the board is unsolvable";
        }
        public static void AddToFile(string path,Solve solve)
        {
            using(StreamWriter streamWriter = new StreamWriter(path,append: true))
            {
                streamWriter.WriteLine("\n" + solve.Board.GetBoardAsString());
            }
        }
    }
}
