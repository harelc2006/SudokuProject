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
            string str = Console.ReadLine();
            Console.WriteLine(str.Length);
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
