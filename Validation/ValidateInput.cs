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
        private int GetLength()
        {
            return Input.Length;
        }
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
        /// the function gets a number and returns if its prime or not
        /// </summary>
        /// <param name="num">the number the function gets</param>
        /// <returns>if the number is prime</returns>
        private bool IsPrime(int num)
        {
            for (int i = 2; i <= num / 2; i++)
            {
                if (num % i == 0)
                {
                    return false;
                }
            }
            return num == 1 ? false : true;
        }
        /// <summary>
        /// the function checks if the board X on X is valid by checking if its prime or not, because 5 on 5 for example 
        /// cannot be a board
        /// </summary>
        private void ValidXByXSize()
        {
            int size = (int)GetSideSize();
            if (IsPrime(size))
            {
                errors.Add(new ValidInputException($"the board cannot be {size}x{size}, since {size} is prime"));
            }
        }
        public void ValidSymbols()
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
                errors.Add(new ValidInputException($"{string.Join(",", notdigits)}"));
            }
        }
    }
}