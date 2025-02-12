

# Sudoku Solver
My project solves Sudoku puzzles **quickly and efficiently** for multiple board sizes:  
**1×1, 4×4, 9×9, 16×16, and 25×25.**
 - [Overview](#overview)
 - [Techniques](#techniques)
 - [How it works](#how-it-works)
- [How to Run](#how-to-run)
- --
## Overview
My project solves sudokus quickly because I use 4 main techniques.

 - Naked singles
 - Hidden singles
 - Naked candidates
 - Backtracking
 
Those four techniques make the sudoku work very fast and get good results.

---
## Techniques

**Naked singles**
if there is a cell that has all the other numbers in its row, column, and box then the only number left to be placed in the cell is the number missing, for example: in a 9×9 board there is a cell and in its row, there are 1,2 and 3 in its column there are 4,5 and 6, and its box there are 7 and 8, then the only possible number for the cell is 9
**Hidden singles**
it checks if there is only one possible location for a number in a box, for example: in a 9 by 9 board there is a box with 2 numbers( x, y) missing which means only two cells are empty in it but one of those cells has x in his row that means that x cannot be placed there, therefore, it should be placed in the other cell
**Naked candidates**
I'll explain how it works on a row but it's the same for a box or column, let's say in the row there are x cells with the same x options in the row, it is safe to say that those options can't be anywhere else in that row, for example, cell1 options are: 1 and 2,cell2 options are: 1,2 and 3 and cell3 options are 2,3 Then 1,2,3 cant be placed anywhere else in the row
**Backtracking**
backtracking is a technique that involves guessing what will be in the cell and then trying to solve it, if it's not possible it goes back and tries another option.

----
## How it works
**Naked singles**
naked singles is not a function in my project but each time there is any placement it updates the options and checks if there is a cell with one option left if so it places it.
**Hidden single**
I've made a function that goes on each cube and tries hidden single on each of his missing numbers.
**Naked candidates**
it contains 3 functions one for row one for column one for box, a main one that calls them all, and one that updates the option if a set is found, it checks if there is a set between two and a number depending on the board size if so it calls a function to remove those options from the row/col/box.
**Backtracking**
If naked singles or hidden singles don't work, it tries backtracking, to make the backtracking faster, I used a function to get the best cell to place each backtracking call will stop when there is an unsolvable board exception or when it tried all the options and non of them stop that way I know the number cant be placed in cell.

---

## How To Run

run git clone https://github.com/harelc2006/SudokuProject.git

open in visual studio 2022

make sure it runs on .net 9

use release mode and run with no debug - for the fastest results
