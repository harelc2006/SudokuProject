using Sudoku.BoardSolving;
using Sudoku.UserHandler;
using System.Diagnostics;

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
            Console.WriteLine("enter board");
            string str = Console.ReadLine();
            while (str != "exit")
            {
                Console.WriteLine();
                str = str.Replace(".", "0");
                Stopwatch sw = Stopwatch.StartNew();
                Console.WriteLine(Output.SolveForTests(str));
                sw.Stop();
                TimeSpan timeSpan = sw.Elapsed;
                Console.WriteLine($"{timeSpan.TotalMilliseconds} : ms");
                Console.WriteLine("enter board");
                str = Console.ReadLine();
            }
            Console.ReadLine();
        }
    }
}
