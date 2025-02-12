using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Tests
{
    /// <summary>
    /// test unsolvable boards
    /// </summary>
    public class TestUnsolvableBoards
    {
        [Fact]
        public void Test1()
        {
            string board = "000030000060000400007050800000406000000900000050010300400000020000300000000000000";
            string expected = "the board is unsolvable";
            board = Sudoku.Manager.SolveForTests(board);
            Assert.Equal(expected, board);
        }
        [Fact]
        public void Test2()
        {
            string board = "000005080000601043000000000010500000000106000300000005530000061000000004000000000";
            string expected = "the board is unsolvable";
            board = Sudoku.Manager.SolveForTests(board);
            Assert.Equal(expected, board);
        }
        [Fact]
        public void Test3()
        {
            string board = "023000009400000100090030040200910004000007800900040002300090001060000000000500000";
            string expected = "the board is unsolvable";
            board = Sudoku.Manager.SolveForTests(board);
            Assert.Equal(expected, board);
        }
        [Fact]
        public void Test4()
        {
            string board = "100006080000700000090050000000560030300000000000003801500001060000020400802005010";
            string expected = "the board is unsolvable";
            board = Sudoku.Manager.SolveForTests(board);
            Assert.Equal(expected, board);
        }
        [Fact]
        public void Test5()
        {
            string board = ";0?0=>010690000000710000500:?0;4000000<0400070=005<3000800000000500@000:?80>10004<30>?8;00=20000>?8;270060000000000000900000000?0000?00000>0=000?3:0000>0026000000;>61029@0<00000100<0@00:40000800500:0?;>012600800?0;0000090<0@0;07000005<00?8:00003050:4080709";
            string expected = "the board is unsolvable";
            board = Sudoku.Manager.SolveForTests(board);
            Assert.Equal(expected, board);
        }
    }
}
