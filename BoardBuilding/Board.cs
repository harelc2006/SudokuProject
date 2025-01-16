using Sudoku.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Calculations
{
    class Board
    {
        private int[,] board;
        /// <summary>
        /// intialize the board
        /// </summary>
        /// <param name="input">the inputed string</param>
        public Board(string input)
        {
            int index = 0;
            int side = (int)Math.Sqrt(input.Length);
            this.board = new int[side, side];
            for(int i = 0; i < side; i++)
            {
                for(int j = 0; j < side; j++)
                {
                    this.board[i, j] = input[index++] - '0';
                }
            }
        }
        private int[,] GetInnerBoxes()
        {
            int side = board.GetLength(0);
            int height_side = 0;
            bool prime = true;
            for(int i = (int)Math.Floor(Math.Sqrt(side)); i >= 2 && prime; i--)
            {
                if(side%i == 0)
                {
                    height_side = i;
                    prime = false;
                }
            }
            int width_side = side/ height_side;
            int[,] boxes = new int[side, 4];
            for(int i = 0; i < boxes.GetLength(0); i++)
            {
                int height = i / height_side;
                int width = i % width_side;
                boxes[i, 0] = height * width_side;
                boxes[i, 1] = (height + 1) * width_side - 1;
                boxes[i, 2] = width * height_side;
                boxes[i, 3] = (width + 1) * height_side - 1;
            }
            return boxes;
        }
    }
}
