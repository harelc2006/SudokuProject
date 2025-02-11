using Sudoku.BoardSolving;
using Sudoku.UserHandler;
using Sudoku.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    /// <summary>
    /// a class that manages the project
    /// </summary>
    public static class Manager
    {
        /// <summary>
        /// handles if its a user
        /// </summary>
        public static void SolveForUsers()
        {
            Input input = new Input();
            string boardStr = input.Start();
            while(boardStr != "exit")
            {
                ValidateInput vi = new ValidateInput(boardStr);
                if (vi.Validate())
                {
                    Solve solve = new Solve(boardStr);
                    if (solve.SolveBoard())
                    {
                        Output.PresentBoardToUser(solve);
                    }
                    else
                    {
                        Output.PresentUnsolvableToUser();
                    }
                    if (input.Type == Input.inputType.file)
                    {
                        Output.AddToFile(input.FilePath, solve);
                    }
                }
                Console.WriteLine();
                boardStr = input.Start();
            }
        }
        /// <summary>
        /// handles for tests
        /// </summary>
        /// <param name="str"></param>
        /// <returns>the board as a string</returns>
        public static string SolveForTests(string str)
        {
            ValidateInput vi = new ValidateInput(str);
            Solve solve = new Solve(str);
            if (solve.SolveBoard())
            {
                return(Output.PresentBoardForTests(solve));
            }
            else
            {
                return(Output.PresentUsolvableBoardForTests());
            }
        }
    }
}
