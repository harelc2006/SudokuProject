using Sudoku.BoardBuilding;
using Sudoku.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.BoardSolving
{
    public class Solve
    {
        private Board board;
        private OptionsManager om;
        private Stack<int[,]> boards;
        private readonly Dictionary<int, int> powersOfTwo;
        private enum opNames { row, col, box };
        public Solve(string input)
        {
            this.board = new Board(input);
            this.om = new OptionsManager(board);
            this.boards = new Stack<int[,]>();
            this.powersOfTwo = new Dictionary<int, int>();
            for (int i = 0; i < board.Side; i++)
            {
                this.powersOfTwo.Add(1 << i, i + 1);
            }
        }
        public Board Board { get => board; }

        public Dictionary<int, int> PowersOfTwo => powersOfTwo;

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
            int number;
            for (int i = 0; i < board.Side; i++)
            {
                int set = (~om.Boxes[i]) & ((1 << board.Side) - 1);
                Stack<int> op = GetOptions(set);
                while(op.Count != 0)
                {
                    number = op.Pop();
                    if ((om.Boxes[i] & (1<<(number-1))) == 0)
                    {
                        (row, col) = PlaceInCube(i, number);
                        if (row != -1)
                        {
                            Place(row, col, i, number);
                            prime = true;
                        }
                    }
                }
            }
            return prime;
        }
        private bool NakedCandidatesAll()
        {
            bool prime = false;
            for (int i = 0; i < board.Side; i++)
            {
                if (NakedCandidatesRow(i) | NakedCandidatesCol(i) | NakedCandidatesBox(i))
                {
                    prime = true;
                }
            }
            return prime;
        }
        private bool NakedCandidatesByRowAndCol(int row, int col)
        {
            return NakedCandidatesRow(row) | NakedCandidatesCol(col) | NakedCandidatesBox(board.GetCube(row, col));
        }
        private bool NakedCandidatesRow(int row)
        {
            for (int i = 2; i <= board.Side/2 +2 ; i++)
            {
                int[] locs = new int[i];
                bool prime = true;
                for (int j = 0; j < board.Side - i && prime; j++)
                {
                    if (board.GetBoard[row, j] != 0)
                        continue;
                    int set = om.Options[row, j];
                    if (om.CountOption[row, j] != i - 1 && om.CountOption[row, j] != i)
                        continue;
                    int count = 1;
                    locs[0] = j;
                    for (int k = j + 1; k < board.Side && prime; k++)
                    {
                        if (board.GetBoard[row, k] != 0)
                            continue;
                        if (om.CountBits(om.Options[row, k] | set) == i)
                        {
                            set |= om.Options[row, k];
                            locs[count++] = k;
                            if (count == i)
                            {
                                prime = false;
                                UpdateOptionsAfterNakedCandidates(locs, set, row, opNames.row);
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        private bool NakedCandidatesCol(int col)
        {
            for (int i = 2; i <= board.Side / 2 + 2; i++)
            {
                int[] locs = new int[i];
                bool prime = true;
                for (int j = 0; j < board.Side - i && prime; j++)
                {
                    if (board.GetBoard[j, col] != 0)
                        continue;
                    int set = om.Options[j, col];
                    if (om.CountOption[j, col] != i - 1 && om.CountOption[j, col] != i)
                        continue;
                    int count = 1;
                    locs[0] = j;
                    for (int k = j + 1; k < board.Side && prime; k++)
                    {
                        if (board.GetBoard[k, col] != 0)
                            continue;
                        if (om.CountBits(om.Options[k, col] | set) == i)
                        {
                            set |= om.Options[k, col];
                            locs[count++] = k;
                            if (count == i)
                            {
                                prime = false;
                                UpdateOptionsAfterNakedCandidates(locs, set, col, opNames.col);
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        private bool NakedCandidatesBox(int cube)
        {
            for (int i = 2; i <= board.Side / 2 + 2; i++)
            {
                int[] locs = new int[i];
                bool prime = true;
                for (int j = 0; j < board.Side - i && prime; j++)
                {
                    int row1 = j / board.InnerBoxWidth + board.InnerBoxes[cube, 0];
                    int col1 = j % board.InnerBoxHeight + board.InnerBoxes[cube, 2];
                    if (board.GetBoard[row1, col1] != 0)
                        continue;
                    int set = om.Options[row1, col1];
                    if (om.CountOption[row1, col1] != i - 1 && om.CountOption[row1, col1] != i)
                        continue;
                    int count = 1;
                    locs[0] = j;
                    for (int k = j + 1; k < board.Side && prime; k++)
                    {
                        int row2 = k / board.InnerBoxWidth + board.InnerBoxes[cube, 0];
                        int col2 = k % board.InnerBoxHeight + board.InnerBoxes[cube, 2];
                        if (board.GetBoard[row2, col2] != 0)
                            continue;
                        if (om.CountBits(om.Options[row2, col2] | set) == i)
                        {
                            set |= om.Options[row2, col2];
                            locs[count++] = k;
                            if (count == i)
                            {
                                prime = false;
                                UpdateOptionsAfterNakedCandidates(locs, set, cube, opNames.box);
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        private void UpdateOptionsAfterNakedCandidates(int[] locs, int set, int num, opNames type)
        {
            if (type == opNames.row)
            {
                for (int i = 0; i < board.Side; i++)
                {
                    if ((!locs.Any(n => n == i)) && board.GetBoard[num, i] == 0)
                    {
                        om.Options[num, i] &= ((~set) & ((1 << board.Side) - 1));
                        if (om.CountBits(om.Options[num, i]) == 1)
                        {
                            Place(num, i, PowersOfTwo[om.Options[num, i]]);
                        }
                        else if (om.CountBits(om.Options[num, i]) == 0)
                        {
                            throw new UnsolvableBoardException();
                        }
                    }
                }
            }
            if (type == opNames.col)
            {
                for (int i = 0; i < board.Side; i++)
                {
                    if ((!locs.Any(n => n == i)) && board.GetBoard[i, num] == 0)
                    {
                        om.Options[i, num] &= ((~set) & ((1 << board.Side) - 1));
                        if (om.CountBits(om.Options[i, num]) == 1)
                        {
                            Place(i, num, PowersOfTwo[om.Options[i, num]]);
                        }
                        else if (om.CountBits(om.Options[i, num]) == 0)
                        {
                            throw new UnsolvableBoardException();
                        }
                    }
                }
            }
            if (type == opNames.box)
            {
                int index = 0;
                for (int i = board.InnerBoxes[num, 0]; i <= board.InnerBoxes[num, 1]; i++)
                {
                    for (int j = board.InnerBoxes[num, 2]; j <= board.InnerBoxes[num, 3]; j++, index++)
                    {
                        if ((!locs.Any(n => n == index)) && board.GetBoard[i, j] == 0)
                        {
                            om.Options[i, j] &= ((~set) & ((1 << board.Side) - 1));
                            if (om.CountBits(om.Options[i, j]) == 1)
                            {
                                Place(i, j, PowersOfTwo[om.Options[i, j]]);
                            }
                            else if (om.CountBits(om.Options[i, j]) == 0)
                            {
                                throw new UnsolvableBoardException();
                            }
                        }
                    }
                }
            }
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
            om.Options[row + board.InnerBoxes[cube, 0], col + board.InnerBoxes[cube, 2]] = 0;
            om.UpdateOptions(row + board.InnerBoxes[cube, 0], col + board.InnerBoxes[cube, 2], num);
            CheckAfterUpdate(row + board.InnerBoxes[cube, 0], col + board.InnerBoxes[cube, 2]);
        }
        private void Place(int row, int col, int num)
        {
            board.GetBoard[row, col] = num;
            om.Options[row, col] = 0;
            om.UpdateOptions(row, col, num);
            CheckAfterUpdate(row, col);
        }
        /// <summary>
        /// After placement the options change in the row,col and box the function checks if there is only one option remains in the
        /// cell if there is only one option left and places it in the right location
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>

        private void CheckAfterUpdate(int row, int col)
        {
            for (int i = 0; i < board.Side; i++)
            {
                if (board.GetBoard[row, i] == 0)
                {
                    if ((om.Options[row, i] & (om.Options[row, i] - 1)) == 0)
                    {
                        int cube = board.GetCube(row, i);
                        int num = PowersOfTwo[om.Options[row, i]];
                        Place(row, i, num);
                    }
                }
            }
            for (int i = 0; i < board.Side; i++)
            {
                if (board.GetBoard[i, col] == 0)
                {
                    if ((om.Options[i, col] & (om.Options[i, col] - 1)) == 0)
                    {
                        int num = PowersOfTwo[om.Options[i, col]];
                        Place(i, col, num);
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
                            int num = PowersOfTwo[om.Options[i, j]];
                            Place(i, j, num);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// the main function of the solving solving the board - right now its incomplete and can only use simple techniques
        /// </summary>
        public bool SolveBoard()
        {
            if (IsSolved())
                return true;
            try
            {
                NakedCandidatesAll();
            }
            catch (UnsolvableBoardException)
            {
                return false;
            }
            return BackTracking();
        }
        private bool BackTracking()
        {
            if (IsSolved())
                return true;
            while (HiddenSingle()) ;
            if (IsSolved())
                return true;
            int row, col;
            (row, col) = om.GetLowestOptions();
            if (row == -1)
                return false;
            Stack<int> op = GetOptions(om.Options[row, col]);
            while (op.Count != 0)
            {
                boards.Push(board.GetBoard.Clone() as int[,]);
                int index = op.Pop();
                try
                {
                    Place(row, col, index);
                    if(board.Side != 25)
                    {
                        NakedCandidatesAll();
                    }
                    if (BackTracking())
                        return true;
                }
                catch (UnsolvableBoardException)
                {

                }
                board.GetBoard = boards.Pop();
                om = new OptionsManager(board);
            }
            return false;
        }
        private Stack<int> GetOptions(int set)
        {
            Stack<int> op = new Stack<int>();
            for (int option = set; option > 0; option &= option - 1)
            {
                op.Push(PowersOfTwo[option & (~(option - 1))]);
            }
            return op;
        }
        private bool IsSolved()
        {
            for(int i = 0; i < board.Side; i++)
            {
                if (om.Rows[i] != (1 << board.Side) - 1)
                    return false;
            }
            return true;
        }
    }
}