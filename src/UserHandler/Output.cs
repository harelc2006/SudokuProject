using Sudoku.BoardSolving;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.UserHandler
{
    public static class Output
    {
        public static void SolveForUser(string str)
        {
            Solve solve = new Solve(str);
            if (solve.SolveBoard())
            {
                solve.Board.PrintBoard();
            }
            else
            {
                Console.WriteLine("the board is unsolvable");
            }
        }
        public static string SolveForTests(string str)
        {
            Solve solve = new Solve(str);
            if (solve.SolveBoard())
            {
                return solve.Board.GetBoardAsString();
            }
            else
            {
                return "the board is unsolvable";
            }
        }
    }
}
