using Sudoku.BoardBuilding;
using Sudoku.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.BoardSolving
{
    class Solve
    {
        private Board board;
        public Solve(string input)
        {
            this.board = new Board(input);
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
            for(int i = 0; i < board.Side; i++)
            {
                counter |= 1 << (board.GetBoard[row, i] - 1);
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
                counter |= 1 << (board.GetBoard[i, col] - 1);
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
                for (int j = board.InnerBoxes[cube, 2]; i <= board.InnerBoxes[cube, 3]; j++)
                {
                    counter |= 1 << (board.GetBoard[i, j] - 1);
                }
            }
            return counter;
        }
        /// <summary>
        /// the function gets a cube number and a number, assuming the number isn't in the cube and checks if the number can be placed in that cube and if it can be 
        /// placed there it returns where if it can't be placed there it returns (-1,-1) the function also checks if there is not
        /// even one possible location for the number it means the board is unsolvable
        /// </summary>
        /// <param name="cube">a number of the cube</param>
        /// <param name="num">the number to check if it can be inserted</param>
        /// <returns>the location of where to put the number or -1,-1 if it can't be placed</returns>
        private (int, int) PlaceInCube(int cube, int num)
        {
            num--;
            bool[] rows = new bool[board.InnerBoxHeight];
            bool[] cols = new bool[board.InnerBoxWidth];
            for (int i = board.InnerBoxes[cube, 0], index = 0; i <= board.InnerBoxes[cube, 1]; i++, index++)
            {
                rows[index] = (GetMissingNumbersInRow(i) & (1 << num)) != (1 << num);
            }
            for (int i = board.InnerBoxes[cube, 2], index = 0; i <= board.InnerBoxes[cube, 3]; i++, index++)
            {
                cols[index] = (GetMissingNumbersInCol(i) & (1 << num)) != (1 << num);
            }
            for (int i = 0; i < board.InnerBoxHeight; i++)
            {
                bool prime = true;
                if (!rows[i])
                {
                    for (int j = 0; j < board.InnerBoxWidth && prime; j++)
                    {
                        prime = board.GetBoard[board.InnerBoxes[cube, 0] + i, board.InnerBoxes[cube, 2] + j] != 0 || cols[j];
                    }
                    rows[i] = prime;
                }
            }
            for (int i = 0; i < board.InnerBoxWidth; i++)
            {
                bool prime = true;
                if (!cols[i])
                {
                    for (int j = 0; j < board.InnerBoxHeight && prime; j++)
                    {
                        prime = board.GetBoard[board.InnerBoxes[cube, 0] + j, board.InnerBoxes[cube, 2] + i] != 0 || rows[j];
                    }
                    cols[i] = prime;
                }
            }
            int false_rows = rows.Count(n => n == false);
            int false_cols = cols.Count(n => n == false);
            if (false_rows == 1 && false_cols == 1)
            {
                return (Array.IndexOf(rows, false), Array.IndexOf(cols, false));
            }
            else if (false_rows == 0 || false_cols == 0)
            {
                throw new UnsolvableBoardException();
            }
            else
            {
                return (-1, -1);
            }
        }
        /// <summary>
        /// the function implements the hidden single method , it gets a number and checks what cube can it be placed in and where
        /// </summary>
        /// <param name="number">the number to insert</param>
        /// <returns>the function returns location to insert the number </returns>
        private (int,int) HiddenSingle(int number)
        {
            int row, col;
            number--;
            for(int i = 0; i < board.Side; i++)
            {
                if((GetMissingNumbersInBox(i) & (1 << number)) != 0)
                {
                    (row, col) = PlaceInCube(i, number + 1);
                    if(row != -1)
                    {
                        return (row, col);
                    }
                }
            }
            return (-1, -1);
        }
    }
}
