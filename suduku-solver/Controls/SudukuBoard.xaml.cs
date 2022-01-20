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

namespace suduku_solver.Controls
{
    public partial class SudukuBoard : UserControl
    {
        private TextBox[,] squares;
        public SudukuBoard()
        {
            InitializeComponent();
            squares = new TextBox[9, 9];
            AddTextBoxesToGrid(squares, this.boardgrid);
        }

        private void AddTextBoxesToGrid(TextBox[,] squares, Grid grid)
        {
            for (int row = 0; row < squares.GetLength(0); row++)
            {
                for (int col = 0; col < squares.GetLength(1); col++)
                {
                    TextBox txtBox = new TextBox();
                    txtBox.SetValue(Grid.RowProperty, row);
                    txtBox.SetValue(Grid.ColumnProperty, col);
                    txtBox.MaxLength = 1;
                    squares[row, col] = txtBox;
                    grid.Children.Add(txtBox);
                }
            }
        }

        public void ClearBoard()
        {
            for (int row = 0; row < squares.GetLength(0); row++)
            {
                for (int col = 0; col < squares.GetLength(1); col++)
                {
                    squares[row, col].Clear();
                }
            }
        }

        public int[,] GetBoard()
        {
            int[,] board = new int[9,9];
            for (int row = 0; row < squares.GetLength(0); row++)
            {
                for (int col = 0; col < squares.GetLength(1); col++)
                {
                    string squareText = squares[row, col].Text;
                    if(squareText.Length == 0)
                    {
                        board[row, col] = 0;
                    }
                    else
                    {
                        board[row, col] = int.Parse(squares[row, col].Text);
                    }
                   
                }
            }
            return board;
        }

        public void SetBoard(int[,] board)
        {
            int[,] clonedBoard = (int[,]) board.Clone();
            for (int row = 0; row < squares.GetLength(0); row++)
            {
                for (int col = 0; col < squares.GetLength(1); col++)
                {
                    squares[row, col].Text = clonedBoard[row, col].ToString();
                }
            }
        }
    }
}
