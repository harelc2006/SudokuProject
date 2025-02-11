using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Exceptions
{
    /// <summary>
    /// invalid input execption
    /// </summary>
    class ValidInputException : Exception
    {
        public ValidInputException(string message) : base(message)
        {

        }
    }
}