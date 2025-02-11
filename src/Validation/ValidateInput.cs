using Sudoku.BoardBuilding;
using Sudoku.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Validation
{
    /// <summary>
    /// a class to validate the input
    /// </summary>
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
            if(GetSideSize() > 25)
            {
                errors.Add(new ValidInputException($"board size can't be over 25x25"));
            }
        }
        /// <summary>
        /// the function checks if the board side's length is valid by checking if it has square root
        /// </summary>
        private void ValidSideSize()
        {
            int size = (int)GetSideSize();
            double sideSize = Math.Sqrt(GetSideSize());
            if (sideSize % 1 != 0)
            {
                errors.Add(new ValidInputException($"the board cannot be {size}x{size}, since {size} has no square root"));
            }
        }
        /// <summary>
        /// the function checks if there are symbols who aren't valid characters
        /// </summary>
        private void ValidSymbols()
        {
            List<char> notdigits = new List<char>();
            bool prime = false;
            foreach (char ch in Input)
            {
                if (ch - '0' < 0 || ch - '0' > GetSideSize())
                {
                    notdigits.Add(ch);
                    prime = true;
                }
            }
            if (prime)
            {
                string pronounce = notdigits.Count > 1 ? "are" : "is";
                errors.Add(new ValidInputException($"{string.Join(",", notdigits)} {pronounce} not valid symbols for this board"));
            }
        }
        /// <summary>
        /// the function checks if the board inserted is valid - no same chars in row/col/box
        /// </summary>
        /// <exception cref="InvalidBoardExecption"></exception>
        private void ValidateBoard()
        {
            Board board = new Board(this.Input);
            int[] rows = new int[board.Side];
            int[] cols = new int[board.Side];
            int[] boxes = new int[board.Side];
            int save;
            for (int i = 0;i < board.Side; i++)
            {
                for(int j = 0; j < board.Side; j++)
                {
                    if(board.GetBoard[i, j] == 0)
                    {
                        continue;
                    }
                    save = rows[i];
                    rows[i] |= (1<<(board.GetBoard[i, j]-1));
                    if (save == rows[i])
                        throw new InvalidBoardExecption();
                    save = cols[j];
                    cols[j] |= (1 << (board.GetBoard[i, j] - 1));
                    if (save == cols[j])
                        throw new InvalidBoardExecption();
                    save = boxes[board.GetCube(i,j)];
                    boxes[board.GetCube(i, j)] |= (1 << (board.GetBoard[i, j] - 1));
                    if (save == boxes[board.GetCube(i, j)])
                        throw new InvalidBoardExecption();
                }
            }
        }
        /// <summary>
        /// the main function of the class , uses all the help function's to test if there if the input is valid and present the right message
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            ValidLength();//checks if the length is valid
            ValidSymbols();//checks if the symbols are valid
            if (errors.Count != 0)
            {
                foreach(Exception ex in errors)
                {
                    Console.WriteLine(ex.Message);
                }
                return false;
            }
            ValidSideSize(); //checks if the inner boxes size is valid
            if (errors.Count != 0)
            {
                foreach (Exception ex in errors)
                {
                    Console.WriteLine(ex.Message);
                }
                return false;
            }
            try
            {
                ValidateBoard(); //validate the soduko board - if its valid to solve
            }
            catch (InvalidBoardExecption ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
    }
}