using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Tests
{
    public class TestHardBoards
    {
        [Fact]
        public void Test1()
        {
            string board = "008000031490000620300090040100009000070800000030027004009200800000004070706908000";
            string expected = "258746931491385627367192548182439765674851392935627184549273816813564279726918453";
            board = Sudoku.Manager.SolveForTests(board);
            Assert.Equal(expected, board);
        }
        [Fact]
        public void Test2()
        {
            string board = "809006000000008070006020000030000805608000140000500060007301080300060000180000700";
            string expected = "819736452523148679476925318732614895658279143941583267267351984394867521185492736";
            board = Sudoku.Manager.SolveForTests(board);
            Assert.Equal(expected, board);
        }
        [Fact]
        public void Test3()
        {
            string board = "000000091009075000540000003005040069060001000002000030006300000000700010270409005";
            string expected = "627834591839175246541692873785243169364981752192567438416358927958726314273419685";
            board = Sudoku.Manager.SolveForTests(board);
            Assert.Equal(expected, board);
        }
        [Fact]
        public void Test4()
        {
            string board = "000100200000080000700000103900005002800079005067000009600520000005007000020010400";
            string expected = "453196278291783564786452193934865712812379645567241389678524931145937826329618457";
            board = Sudoku.Manager.SolveForTests(board);
            Assert.Equal(expected, board);
        }
        [Fact]
        public void Test5()
        {
            string board = "000200007290000061000410000000000000037050000405003208000100000000005300180060040";
            string expected = "841236957293578461576419823928741635637852194415693278359124786764985312182367549";
            board = Sudoku.Manager.SolveForTests(board);
            Assert.Equal(expected, board);
        }
        [Fact]
        public void Test6()
        {
            string board = "000900640007060029000000000006050001200600500073100060000010000400090007128000003";
            string expected = "312987645857461329694325718946752831281639574573148962739814256465293187128576493";
            board = Sudoku.Manager.SolveForTests(board);
            Assert.Equal(expected, board);
        }
        [Fact]
        public void Test7()
        {
            string board = "000050907000700300000004586080002005000600000709000008007090000160030040003500000";
            string expected = "416853927598726314372914586681372495254689731739145268827491653165237849943568172";
            board = Sudoku.Manager.SolveForTests(board);
            Assert.Equal(expected, board);
        }
        [Fact]
        public void Test8()
        {
            string board = "002080700905000000004200060000300000490002000000100870050000090000000026026970040";
            string expected = "632589714975641283184237965217368459498752631563194872851426397749813526326975148";
            board = Sudoku.Manager.SolveForTests(board);
            Assert.Equal(expected, board);
        }
        [Fact]
        public void Test9()
        {
            string board = "300070090200001000000090100030000500009000472800050009000007020010035000520000060";
            string expected = "381576294294381756765492183436729518159863472872154639943617825618235947527948361";
            board = Sudoku.Manager.SolveForTests(board);
            Assert.Equal(expected, board);
        }
        [Fact]
        public void Test10()
        {
            string board = "057026000061300005000095008005080007040000000000700430009070000470000800008003050";
            string expected = "957826143861347925234195678395684217746231589182759436519478362473562891628913754";
            board = Sudoku.Manager.SolveForTests(board);
            Assert.Equal(expected, board);
        }
    }
}
