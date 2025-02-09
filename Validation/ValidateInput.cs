using Sudoku.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Validation
{
    class ValidateInput
    {
        private string input; //the string inputed
        private List<ValidInputException> errors; //list containing the errors
        /// <summary>
        /// Build Function for ValidateInput
        /// </summary>
        /// <param name="input"></param>
        public ValidateInput(string input)
        {
            this.input = input;
            this.errors = new List<ValidInputException>();
        }
        /// <summary>
        /// returns the input
        /// </summary>
        private string Input { get => input; }
        /// <summary>
        /// the function returns the length of the input
        /// </summary>
        /// <returns>the length of the input</returns>
        private int GetLength()
        {
            return Input.Length;
        }
        /// <summary>
        /// the function returns the length of the side of the board
        /// </summary>
        /// <returns>the length of the side of the board</returns>
        private double GetSideSize()
        {
            return Math.Sqrt(GetLength());
        }
        /// <summary>
        /// the function tests if the sudoku has a valid length meaning if the sudolu has a natural square root
        /// </summary>
        private void ValidLength()
        {
            if (GetSideSize() % 1 != 0)
            {
                errors.Add(new ValidInputException($"{GetLength()} is not a valid length for a sudoku board"));
            }
        }
        /// <summary>
        /// the function checks if the board side's length is valid by checking if its prime or not, because 5 on 5 for example 
        /// cannot be a board
        /// </summary>
        private void ValidSideSize()
        {
            int size = (int)GetSideSize();
            if (Math.Sqrt(size) % 1 == 0)
            {
                errors.Add(new ValidInputException($"the board cannot be {size}x{size}, since {size} has no square root"));
            }
        }
        /// <summary>
        /// the function checks if there are symbols who aren't digits or are bigger than the size of the board
        /// </summary>
        private void ValidSymbols()
        {
            List<char> notdigits = new List<char>();
            bool prime = false;
            foreach (char ch in Input)
            {
                if (ch - '0' <= 0 && ch - '0' > GetSideSize())
                {
                    notdigits.Add(ch);
                    prime = true;
                }
            }
            if (prime)
            {
                errors.Add(new ValidInputException($"{string.Join(",", notdigits)} are not valid symbols for this board"));
            }
        }
    }
}