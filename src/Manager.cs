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
    public static class Manager
    {
        public static void SolveForUsers()
        {
            Input input = new Input();
            string boardStr = input.Start();
            ValidateInput vi = new ValidateInput(boardStr);
            Solve solve = new Solve(boardStr);
            if (solve.SolveBoard())
            {
                Output.PresentBoardToUser(solve);
            }
            else
            {
                Output.PresentUnsolvableToUser();
            }
            if(input.Type == Input.inputType.file)
            {
                Output.AddToFile(input.FilePath, solve);
            }
        }
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
