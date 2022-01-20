using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;


namespace suduku_solver
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            this.boardView.ClearBoard();
        }

        private void BtnSolve_Click(object sender, RoutedEventArgs e)
        {
            int[,] puzzle = this.boardView.GetBoard();
            if (PreCheck(puzzle))
            {
                if (SolveSudoku(puzzle, 0, 0))
                {
                    boardView.SetBoard(puzzle);
                    this.resultText.Text = "Solution Found!";
                }
            }
            else
            {
                this.resultText.Text = "User input breaking rules";
            }
        }

        public static bool SolveSudoku(int[,] puzzle, int row, int col)
        {
            if (row < 9 && col < 9)
            {
                if (puzzle[row, col] != 0)
                {
                    if ((col + 1) < 9) return SolveSudoku(puzzle, row, col + 1);
                    else if ((row + 1) < 9) return SolveSudoku(puzzle, row + 1, 0);
                    else return true;
                }
                else
                {
                    for (int i = 0; i < 9; ++i)
                    {
                        if (AmountErrors(puzzle, row, col, i + 1) == 0)
                        {
                            puzzle[row, col] = i + 1;

                            if ((col + 1) < 9)
                            {
                                if (SolveSudoku(puzzle, row, col + 1)) return true;
                                else puzzle[row, col] = 0;
                            }
                            else if ((row + 1) < 9)
                            {
                                if (SolveSudoku(puzzle, row + 1, 0)) return true;
                                else puzzle[row, col] = 0;
                            }
                            else return true;
                        }
                    }
                }
                return false;
            }
            else return true;
        }

        private static int AmountErrors(int[,] puzzle, int row, int col, int num)
        {
            int errorCount = 0;
            int rowStart = (row / 3) * 3;
            int colStart = (col / 3) * 3;

            for (int i = 0; i < 9; ++i)
            {
                if (puzzle[row, i] == num) errorCount++;
                if (puzzle[i, col] == num) errorCount++;
                if (puzzle[rowStart + (i % 3), colStart + (i / 3)] == num) errorCount++;
            }

            return errorCount;
        }

        private static bool PreCheck(int[,] puzzle)
        {
            for (int row = 0; row < 9; row++)
            {
                for(int col = 0; col < 9; col++)
                {
                    int val = puzzle[row, col];
                    if (val != 0 && AmountErrors(puzzle, col, row, val) > 3)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
