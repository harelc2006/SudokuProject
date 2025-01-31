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
        private OptionsManager om;
        public Solve(string input)
        {
            this.board = new Board(input);
            this.om = new OptionsManager(board);
        }
        public Board Board { get => board; }
        
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
                rows[index] = (om.Rows[i] & (1 << num)) != 0;
            }
            for (int i = board.InnerBoxes[cube, 2], index = 0; i <= board.InnerBoxes[cube, 3]; i++, index++)
            {
                cols[index] = (om.Cols[i] & (1 << num)) != 0;
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
        /// <returns>the function returns location to insert the number and the cube</returns>
        private bool HiddenSingle()
        {
            int row, col;
            bool prime = false;
            for (int number=0;number< board.Side; number++)
            {
                for (int i = 0; i < board.Side; i++)
                {
                    if ((om.Boxes[i] & (1 << number)) == 0)
                    {
                        (row, col) = PlaceInCube(i, number + 1);
                        if (row != -1)
                        {
                            Place(row, col, i, number + 1);
                            prime = true;
                        }
                    }
                }
            }
            return prime;
        }
        /// <summary>
        /// the function  gets a row ,col,cube and number and places the number in the location
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="cube"></param>
        /// <param name="num"></param>
        private void Place(int row, int col, int cube, int num)
        {
            board.GetBoard[row + board.InnerBoxes[cube, 0], col + board.InnerBoxes[cube, 2]] = num;
            om.UpdateOptions(row + board.InnerBoxes[cube, 0], col + board.InnerBoxes[cube, 2], num);
            CheckAfterUpdate(row + board.InnerBoxes[cube, 0], col + board.InnerBoxes[cube, 2]);
        }
        
        private void CheckAfterUpdate(int row, int col)
        {
            List<int[]> places = new List<int[]>();
            for(int i = 0; i < board.Side; i++)
            {
                if(board.GetBoard[row,i] == 0)
                {
                    if((om.Options[row,i]& (om.Options[row,i] - 1)) == 0)
                    {
                        int cube = board.GetCube(row, i);
                        int r = row - board.InnerBoxes[cube, 0];
                        int c = i - board.InnerBoxes[cube, 2];
                        int num = (int)(Math.Log(om.Options[row, i]) / Math.Log(2)) + 1;
                        int[] tmp = {r,c,cube,num};
                        places.Add(tmp);
                    }
                }
            }
            for (int i = 0; i < board.Side; i++)
            {
                if (board.GetBoard[i, col] == 0)
                {
                    if ((om.Options[i, col] & (om.Options[i, col] - 1)) == 0)
                    {
                        int cube = board.GetCube(i, col);
                        int r = i - board.InnerBoxes[cube, 0];
                        int c = col - board.InnerBoxes[cube, 2];
                        int num = (int)(Math.Log(om.Options[i, col]) / Math.Log(2)) + 1;
                        int[] tmp = { r, c, cube, num };
                        places.Add(tmp);
                    }
                }
            }
            for (int i = board.InnerBoxes[board.GetCube(row, col), 0]; i <= board.InnerBoxes[board.GetCube(row, col), 1]; i++)
            {
                for (int j = board.InnerBoxes[board.GetCube(row, col), 2]; j <= board.InnerBoxes[board.GetCube(row, col), 3]; j++)
                {
                    if (board.GetBoard[i, j] == 0)
                    {
                        if ((om.Options[i, j] & (om.Options[i, j] - 1)) == 0)
                        {
                            int cube = board.GetCube(i, j);
                            int r = i - board.InnerBoxes[cube, 0];
                            int c = j - board.InnerBoxes[cube, 2];
                            int num = (int)(Math.Log(om.Options[i, j]) / Math.Log(2)) + 1;
                            int[] tmp = { r, c, cube, num };
                            places.Add(tmp);
                        }
                    }
                }
            }
            for(int i = 0; i < places.Count; i++)
            {
                Place(places[i][0], places[i][1], places[i][2], places[i][3]);
            }
        }
                /// <summary>
        /// the main function of the solving solving the board - right now its incomplete and can only use simple techniques
        /// </summary>
        public bool BackTracking()
        {
            if (board.GetBoard.Cast<int>().All(n => n != 0))
                return true;

            int row, col;
            (row, col) = om.GetLowestOptions();
            int cube = board.GetCube(row, col);
            if (row == -1)
                return false;
            for (int option = om.Options[row, col], index = 1; option > 0; option = option >> 1, index++)
            {
                if ((option & 1) == 1)
                {
                    int[,] copy = board.GetBoard.Clone() as int[,];
                    try
                    {
                        Place(row - board.InnerBoxes[cube, 0], col - board.InnerBoxes[cube, 2], cube, index);
                        if (BackTracking())
                            return true;
                    }
                    catch (Exception)
                    {

                    }
                    board.GetBoard = copy.Clone() as int[,];
                    om = new OptionsManager(board);
                    om.Options[row, col] &= (~(1 << (index - 1))) & ((1 << board.Side) - 1);
                }
            }
            return false;
        }
    }
}
