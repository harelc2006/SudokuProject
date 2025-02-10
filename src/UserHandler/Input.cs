using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.UserHandler
{
    public class Input
    {
        public enum inputType {user,file};
        private inputType type;
        private string filePath;

        public inputType Type { get => type; set => type = value; }
        public string FilePath { get => filePath; set => filePath = value; }

        public Input()
        {
            this.type = inputType.user;
            this.filePath = "";
        }
        public string Start()
        {
            string message = "Click 1 : for typing your sudoku board mannually\n" +
                "Click 2 : for reading it out of a filePress enter when you made your choice";
            Console.WriteLine(message);
            string choice = Console.ReadLine();
            while (!(choice.Equals("1") || choice.Equals("2")))
            {
                Console.WriteLine("please enter 1 or 2");
                Console.WriteLine(message);
                choice = Console.ReadLine();
            }
            if (choice.Equals("1"))
            {
                return(ReadFromUser());
            }
            else
            {
                return (ReadFromFile());
            }

        }
        private string ReadFromUser()
        {
            Console.WriteLine("Please enter your soduko board");
            string board = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(board))
            {
                Console.WriteLine("not a valid input\n");
                return Start();
            }
            this.type = inputType.user;
            return board;
        }
        private string ReadFromFile()
        {
            Console.WriteLine("Please enter you file path with no \"");
            string path = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(path))
            {
                if (File.Exists(path))
                {
                    StreamReader streamReader = new StreamReader(path);
                    string board = streamReader.ReadLine();
                    if (string.IsNullOrWhiteSpace(board))
                    {
                        Console.WriteLine("not a valid input\n");
                        return Start();
                    }
                    streamReader.Close();
                    this.type = inputType.file;
                    this.filePath = path;
                    return board;
                }
                else
                {
                    Console.WriteLine("no file found\n");
                    return Start();
                }
            }
            else
            {
                Console.WriteLine("file path not valid\n");
                return(Start());
            }
        }
    }
}
