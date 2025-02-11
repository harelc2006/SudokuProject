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
    /// <summary>
    /// a class to handle the output
    /// </summary>
    public static class Output
    {
        /// <summary>
        /// presents the board to the user
        /// </summary>
        /// <param name="solve"></param>
        public static void PresentBoardToUser(Solve solve)
        {
            solve.Board.PrintBoard();
        }
        /// <summary>
        /// if the board is unsolvable it presents to the user
        /// </summary>
        public static void PresentUnsolvableToUser()
        {
            Console.WriteLine("the board is unsolvable");
        }
        /// <summary>
        /// presents the board for tests
        /// </summary>
        /// <param name="solve"></param>
        /// <returns>the board as string</returns>
        public static string PresentBoardForTests(Solve solve)
        {
            return solve.Board.GetBoardAsString();
        }
        /// <summary>
        /// presents unsolvable board for tests
        /// </summary>
        /// <returns>the message</returns>
        public static string PresentUsolvableBoardForTests()
        {
            return "the board is unsolvable";
        }
        /// <summary>
        /// adds the board as a string to the file in the path
        /// </summary>
        /// <param name="path">the path</param>
        /// <param name="solve"></param>
        public static void AddToFile(string path,Solve solve)
        {
            using(StreamWriter streamWriter = new StreamWriter(path,append: true))
            {
                streamWriter.WriteLine("\n" + solve.Board.GetBoardAsString());
            }
        }
    }
}
