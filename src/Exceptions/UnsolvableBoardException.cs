using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Exceptions
{
    /// <summary>
    /// unsolvable board execption
    /// </summary>
    class UnsolvableBoardException : Exception
    {
        public UnsolvableBoardException() : base("the board is unsolvable")
        {

        }
    }
}
