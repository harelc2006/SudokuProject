using Sudoku.BoardBuilding;
using Sudoku.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.BoardSolving
{
    class OptionsManager
    {
        private Board board;
        private int[] rows;
        private int[] cols;
        private int[] boxes;
        private int[,] options;

        public int[] Rows { get => rows; }
        public int[] Cols { get => cols; }
        public int[] Boxes { get => boxes; }
        public int[,] Options { get => options; }


        public OptionsManager(Board board)
        {
            this.board = board;
            this.rows = new int[board.Side];
            this.cols = new int[board.Side];
            this.boxes = new int[board.Side];
            this.options = new int[board.Side, board.Side];
            for (int i = 0; i < board.Side; i++)
            {
                Rows[i] = GetMissingNumbersInRow(i);
            }
            for (int i = 0; i < board.Side; i++)
            {
                Cols[i] = GetMissingNumbersInCol(i);
            }
            for (int i = 0; i < board.Side; i++)
            {
                Boxes[i] = GetMissingNumbersInBox(i);
            }
            IntializeOptions();
        }
        /// <summary>
        /// the function gets a row and returns the missing numbers in the row
        /// the missing numbers are returned as an int and if the first bit is on it means the number one is in the row and so on
        /// </summary>
        /// <param name="row">the row to check</param>
        /// <returns>integer who's bit signal which number is in the row</returns>
        public int GetMissingNumbersInRow(int row)
        {
            int counter = 0;
            for (int i = 0; i < board.Side; i++)
            {
                if (board.GetBoard[row, i] != 0)
                {
                    counter |= 1 << (board.GetBoard[row, i] - 1);
                }
            }
            return counter;
        }
        /// <summary>
        /// the function gets a col and returns the missing numbers in the col
        /// the missing numbers are returned as an int and if the first bit is on it means the number one is in the col and so on        /// </summary>
        /// <param name="col">the col to check</param>
        /// <returns>integer who's bit signal which number is in the col</returns>
        public int GetMissingNumbersInCol(int col)
        {
            int counter = 0;
            for (int i = 0; i < board.Side; i++)
            {
                if (board.GetBoard[i, col] != 0)
                {
                    counter |= 1 << (board.GetBoard[i, col] - 1);
                }
            }
            return counter;
        }
        /// <summary>
        /// the function gets a cube and returns the missing numbers in the cube
        /// the missing numbers are returned as an int and if the first bit is on it means the number one is in the cube and so on          /// </summary>
        /// <param name="cube">the cube to check</param>
        /// <returns>integer who's bit signal which number is in the cube</returns>
        private int GetMissingNumbersInBox(int cube)
        {
            int counter = 0;
            for (int i = board.InnerBoxes[cube, 0]; i <= board.InnerBoxes[cube, 1]; i++)
            {
                for (int j = board.InnerBoxes[cube, 2]; j <= board.InnerBoxes[cube, 3]; j++)
                {
                    if (board.GetBoard[i, j] != 0)
                    {
                        counter |= 1 << (board.GetBoard[i, j] - 1);
                    }
                }
            }
            return counter;
        }
        /// <summary>
        /// the function returns options for by the index of
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns>options to put in this index if the first bit is on then the number one can be placed there</returns>
        public int GetOptions(int row, int col)
        {
            int cube = board.GetCube(row, col);
            return ~(Rows[row] | Cols[col] | Boxes[cube]) & ((1 << board.Side) - 1);
        }
        public void UpdateOptions(int row, int col, int number)
        {
            int cube = board.GetCube(row, col);
            rows[row] |= (1 << (number - 1));
            cols[col] |= (1 << (number - 1));
            boxes[cube] |= (1 << (number - 1));
            for (int i = 0; i < board.Side; i++)
            {
                if(board.GetBoard[row, i] == 0)
                {
                    Options[row, i] &= ~(1 << (number - 1));
                    if (Options[row, i] == 0)
                    {
                        throw new UnsolvableBoardException();
                    }
                }
            }
            for (int i = 0; i < board.Side; i++)
            {
                if(board.GetBoard[i, col] == 0)
                {
                    Options[i, col] &= ~(1 << (number - 1));
                    if(Options[i,col] == 0)
                    {
                        throw new UnsolvableBoardException();
                    }
                }
            }
            for (int i = board.InnerBoxes[cube, 0]; i <= board.InnerBoxes[cube, 1]; i++)
            {
                for (int j = board.InnerBoxes[cube, 2]; j <= board.InnerBoxes[cube, 3]; j++)
                {
                    if(board.GetBoard[i, j] == 0)
                    {
                        Options[i, j] &= ~(1 << (number - 1)); ;
                        if (Options[i, j] == 0)
                        {
                            throw new UnsolvableBoardException();
                        }
                    }
                }
            }
        }
        public void IntializeOptions()
        {
            for (int i = 0; i < board.Side; i++)
            {
                for (int j = 0; j < board.Side; j++)
                {
                    if (board.GetBoard[i, j] == 0)
                    {
                        options[i, j] = GetOptions(i, j);
                    }
                }
            }
        }
        public (int, int) GetLowestOptions()
        {
            int min = 10, row = 0, col = 0;
            for (int i = 0; i < board.Side; i++)
            {
                for (int j = 0; j < board.Side; j++)
                {
                    if (board.GetBoard[i, j] == 0)
                    {
                        int number = options[i, j], count = 0;
                        while (number > 0)
                        {
                            number &= number - 1;
                            count++;
                        }
                        if (min > count)
                        {
                            min = count;
                            row = i;
                            col = j;
                        }
                    }
                }
            }
            if(min == 10)
            {
                return (-1, -1);
            }
            return (row, col);
        }
        public void PrintOptions()
        {
            for (int i = 0; i < board.Side; i++)
            {
                for (int j = 0; j < board.Side; j++)
                {
                    Console.Write(Convert.ToString(options[i, j] >= 0 ? options[i, j] : 0, 2) + " ");
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
