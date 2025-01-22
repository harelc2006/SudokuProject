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
        /// the function gets either a row/col and tests which number are in the row/col
        /// if you want to test col you put -1 in row and vice versa
        /// </summary>
        /// <param name="row">-1 or the row that needs to be tested</param>
        /// <param name="col">-1 or the col that needs to be tested</param>
        /// <returns>if the first bit is on then the number exists and so on</returns>
        private int GetMissingNumbers(int row, int col)
        {
            int counter = 0;
            int r_add = row == -1 ? 1 : 0;
            int c_add = col == -1 ? 1 : 0;
            int r_index = row == -1 ? 0 : row, c_index = col == -1 ? 0 : col;
            while (r_index < board.GetBoard.GetLength(0) && c_index < board.GetBoard.GetLength(0))
            {
                if (board.GetBoard[r_index, c_index] > 0)
                {
                    counter |= 1 << (board.GetBoard[r_index, c_index] - 1);
                }
                r_index += r_add;
                c_index += c_add;
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
            bool[] rows = new bool[board.Height];
            bool[] cols = new bool[board.Width];
            for (int i = board.InnerBoxes[cube, 0], index = 0; i <= board.InnerBoxes[cube, 1]; i++, index++)
            {
                rows[index] = (GetMissingNumbers(i, -1) & (1 << num)) != (1 << num);
            }
            for (int i = board.InnerBoxes[cube, 2], index = 0; i <= board.InnerBoxes[cube, 3]; i++, index++)
            {
                cols[index] = (GetMissingNumbers(-1, i) & (1 << num)) != (1 << num);
            }
            for (int i = 0; i < board.Height; i++)
            {
                bool prime = true;
                if (!rows[i])
                {
                    for (int j = 0; j < board.Width && prime; j++)
                    {
                        prime = board.GetBoard[board.InnerBoxes[cube, 0] + i, board.InnerBoxes[cube, 2] + j] != 0 || cols[j];
                    }
                    rows[i] = prime;
                }
            }
            for (int i = 0; i < board.Width; i++)
            {
                bool prime = true;
                if (!cols[i])
                {
                    for (int j = 0; j < board.Height && prime; j++)
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
    }
}
