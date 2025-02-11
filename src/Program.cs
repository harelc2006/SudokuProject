using Sudoku.BoardSolving;
using Sudoku.UserHandler;
using System.Diagnostics;

namespace Sudoku
{
    class Program
    {
        /// <summary>
        /// calls for solve for user
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Manager.SolveForUsers();
        }
    }
}
