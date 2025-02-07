using Sudoku.BoardSolving;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Program
    {
        /// <summary>
        /// is being used to test the program 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string str = "000005080000601043000000000010500000000106000300000005530000061000000004000000000";
            str = str.Replace(".", "0");
            Solve solve = new Solve(str);
            solve.Board.PrintBoard();
            Console.WriteLine("\n\n\n");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine(solve.SolveBoard());
            stopwatch.Stop();
            TimeSpan elapsed = stopwatch.Elapsed;
            Console.WriteLine($"Elapsed time: {elapsed.TotalMilliseconds} ms");
            solve.Board.PrintBoard();
            Console.ReadLine();
        }
    }
}
