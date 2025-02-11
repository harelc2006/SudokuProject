using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Exceptions
{
    internal class InvalidBoardExecption : Exception
    {
        public InvalidBoardExecption() : base("this board is invalid")
        {

        }
    }
}
