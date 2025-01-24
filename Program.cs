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
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string str = "000870400090004600600009012000040751201007098000300000002010003700003000850900040";
            Solve solve = new Solve(str);
            solve.Board.PrintBoard();
            Console.WriteLine("\n\n\n");
            solve.SolveBoard();
            solve.Board.PrintBoard();
            stopwatch.Stop();
            TimeSpan elapsed = stopwatch.Elapsed;
            Console.WriteLine($"Elapsed time: {elapsed.TotalMilliseconds} ms");
            Console.ReadLine();
        }
    }
}
