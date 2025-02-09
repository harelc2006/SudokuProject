using Sudoku.BoardBuilding;
using Sudoku.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.BoardSolving
{
    /// <summary>
    /// this class is used manage the options of the board
    /// </summary>
    class OptionsManager
    {
        private Board board;
        private int[] rows;
        private int[] cols;
        private int[] boxes;
        private int[,] options;
        private int[,] countOption;
        /// <summary>
        /// get functions for the variables
        /// </summary>
        public int[] Rows { get => rows; set => rows = value; }
        public int[] Cols { get => cols; set => cols = value; }
        public int[] Boxes { get => boxes; set => boxes = value; }
        public int[,] Options { get => options; set => options = value; }
        public int[,] CountOption { get => countOption; set => countOption = value; }

        public OptionsManager(Board board)
        {
            this.board = board;
            this.rows = new int[board.Side];
            this.cols = new int[board.Side];
            this.boxes = new int[board.Side];
            this.options = new int[board.Side, board.Side];
            this.countOption = new int[board.Side, board.Side];
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
        /// <summary>
        /// this function update the options in the row , col and box acording to the number inserted
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="number"></param>
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
                    if((Options[row, i] & 1<<(number-1)) != 0)
                    {
                        countOption[row, i]--;
                    }
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
        /// <summary>
        /// the function intialize the options array
        /// </summary>
        public void IntializeOptions()
        {
            for (int i = 0; i < board.Side; i++)
            {
                for (int j = 0; j < board.Side; j++)
                {
                    if (board.GetBoard[i, j] == 0)
                    {
                        options[i, j] = GetOptions(i, j);
                        countOption[i, j] = CountBits(options[i, j]);
                    }
                }
            }
        }
        private int GetImpact(int row,int col)
        {
            int impact = 0;
            int cube = board.GetCube(row, col);
            int set = Options[row, col];
            for(int i = 0; i < board.Side; i++)
            {
                impact += CountBits(Options[row, i] & set);
            }
            for (int i = 0; i < board.Side; i++)
            {
                impact += CountBits(Options[i, col] & set);
            }
            for (int i = 0; i < board.Side; i++)
            {
                int row1 = i / board.InnerBoxWidth + board.InnerBoxes[cube, 0];
                int col1 = i % board.InnerBoxHeight + board.InnerBoxes[cube, 2];
                impact += CountBits(Options[row1, col1] & set);
            }
            return (impact - (3 * CountBits(set)));
        }
        /// <summary>
        /// the function returns the cell with the lowest amount of options 
        /// </summary>
        /// <returns>the row,col or -1,-1 if there is no cell with the lowest options found</returns>
        public (int, int) GetLowestOptions()
        {
            int min = 10, row = 0, col = 0,impact = 0;
            for (int i = 0; i < board.Side; i++)
            {
                for (int j = 0; j < board.Side; j++)
                {
                    if (board.GetBoard[i, j] == 0)
                    {
                        int count = countOption[i,j];
                        if (min > count)
                        {
                            min = count;
                            row = i;
                            col = j;
                            impact = GetImpact(row, col);
                        }
                        if(min == count)
                        {
                            int saveImpact = GetImpact(i, j);
                            if(saveImpact > impact)
                            {
                                impact = saveImpact;
                                row = i;
                                col = j;
                            }
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
        public int CountBits(int num)
        {
            int count = 0;
            while (num > 0)
            {
                num &= num - 1;
                count++;
            }
            return count;
        }
        public void PrintOptions()
        {
            for (int i = 0; i < board.Side; i++)
            {
                for (int j = 0; j < board.Side; j++)
                {
                    string op = Convert.ToString(options[i, j] >= 0 ? options[i, j] : 0, 2);
                    for (int k = 0; k < board.Side - op.Length; k++)
                    {
                        Console.Write(" ");
                    }
                    Console.Write(op);
                    if (j % 3 == 2)
                    {
                        Console.Write("| ");
                    }
                    Console.Write(" ");
                }
                Console.WriteLine();
                if (i % 3 == 2)
                {
                    Console.WriteLine("----------------------------------------------------------------------------------------------");
                }
            }
        }
    }
}
