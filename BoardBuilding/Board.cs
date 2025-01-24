using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.BoardBuilding
{
    class Board
    {
        private int[,] board;
        private int[,] inner_boxes;
        private int side,height_of_inner_box, width_of_inner_box;
        /// <summary>
        /// intialize the board
        /// </summary>
        /// <param name="input">the inputed string</param>
        public Board(string input)
        {
            int index = 0;
            this.side = (int)Math.Sqrt(input.Length);
            this.board = new int[side, side];
            for (int i = 0; i < side; i++)
            {
                for (int j = 0; j < side; j++)
                {
                    this.board[i, j] = input[index++] - '0';
                }
            }
            this.inner_boxes = GetInnerBoxes();
        }

        public int[,] GetBoard { get => board; }
        public int[,] InnerBoxes { get => inner_boxes; }
        public int InnerBoxHeight { get => height_of_inner_box; }
        public int InnerBoxWidth { get => width_of_inner_box; }
        public int Side { get => side; }

        /// <summary>
        /// the function returns a 2d array with every row represents an inner box by top, bottom, left ,right  
        /// </summary>
        /// <returns>2d array containing the inner boxes coordinates</returns>
        private int[,] GetInnerBoxes()
        {
            int side = board.GetLength(0);
            int height_side = 0;
            bool prime = true;
            for (int i = (int)Math.Floor(Math.Sqrt(side)); i >= 2 && prime; i--)
            {
                if (side % i == 0)
                {
                    height_side = i;
                    prime = false;
                }
            }
            int width_side = side / height_side;
            this.height_of_inner_box = height_side;
            this.width_of_inner_box = width_side;
            int[,] boxes = new int[side, 4];
            int index = 0;
            for (int i = 0; i < width_side; i++)
            {
                for (int j = 0; j < height_side; j++)
                {
                    boxes[index, 0] = i * height_side;
                    boxes[index, 1] = (i + 1) * height_side - 1;
                    boxes[index, 2] = j * width_side;
                    boxes[index++, 3] = (j + 1) * width_side - 1;
                }
            }
            return boxes;
        }
        /// <summary>
        /// print the board
        /// </summary>
        public void PrintBoard()
        {
            for (int i = 0; i < Side; i++)
            {
                for (int j = 0; j < Side; j++)
                {
                    Console.Write(GetBoard[i,j] + " ");
                    if (j % 3 == 2)
                    {
                        Console.Write("| ");
                    }
                }
                Console.WriteLine();
                if (i % 3 == 2)
                {
                    Console.WriteLine("---------------------");
                }
            }
        }
    }
}
