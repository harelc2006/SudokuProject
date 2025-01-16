using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] numbers = { { 1, 4, 2}, { 3, 6, 8 } ,{1,4,2 } };
            Console.WriteLine(numbers.Length);
            Console.ReadLine();
        }
    }
}
