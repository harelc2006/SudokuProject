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

        public int[,] GetBoard { get => board; set => board = value; }
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
            int height_side = (int)Math.Sqrt(side);
            bool prime = true;
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
        public int GetCube(int i,int j)
        {
            return (i / InnerBoxHeight * InnerBoxWidth + (j / InnerBoxWidth % InnerBoxHeight));
        }
        /// <summary>
        /// print the board
        /// </summary>
        public void PrintBoard()
        {
            int size = board.GetLength(0);
            int boxSize = (int)Math.Sqrt(size);

            if (boxSize * boxSize != size)
            {
                Console.WriteLine("Invalid Sudoku size.");
                return;
            }

            string horizontalLine = " " + new string('-', size * 2 + boxSize + 1);

            for (int row = 0; row < size; row++)
            {
                if (row % boxSize == 0)
                    Console.WriteLine(horizontalLine);

                for (int col = 0; col < size; col++)
                {
                    if (col % boxSize == 0)
                        Console.Write("| ");

                    Console.Write(board[row, col] == 0 ? "0 " : (char)('0'+board[row, col]) + " ");
                }

                Console.WriteLine("|");
            }

            Console.WriteLine(horizontalLine);
        }
    }
}
