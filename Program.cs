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
            string str = "1.....3.8.7.4..............2.3.1...........958.........5.6...7.....8.2...4.......";
            str = str.Replace(".", "0");
            Solve solve = new Solve(str);
            solve.Board.PrintBoard();
            Console.WriteLine("\n\n\n");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine(solve.BackTracking()); 
            solve.Board.PrintBoard();
            stopwatch.Stop();
            TimeSpan elapsed = stopwatch.Elapsed;
            Console.WriteLine($"Elapsed time: {elapsed.TotalMilliseconds} ms");
            Console.ReadLine();
        }
    }
}
