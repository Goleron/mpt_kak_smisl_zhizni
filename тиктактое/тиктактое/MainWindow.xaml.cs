using System;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToeGame
{
    public partial class MainWindow : Window
    {
        private bool isPlayerTurn = true; 
        private int[] board = new int[9]; 

        public MainWindow()
        {
            InitializeComponent();
            RestartGame();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int buttonIndex = Convert.ToInt32(button.Name.Substring(6));

            if (board[buttonIndex] == 0 && isPlayerTurn) 
            {
                button.Content = "X";
                board[buttonIndex] = 1;
                isPlayerTurn = false;
                CheckForWinner();

                if (!isPlayerTurn) 
                {
                    MakeComputerMove();
                }
            }
        }

        private void MakeComputerMove()
        {
            Random rnd = new Random();
            var emptyCells = new System.Collections.Generic.List<int>();

            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == 0)
                {
                    emptyCells.Add(i);
                }
            }

            if (emptyCells.Count > 0)
            {
                int index = emptyCells[rnd.Next(emptyCells.Count)];
                board[index] = 2; 
                ((Button)FindName($"Button{index}")).Content = "O";
                isPlayerTurn = true; 
                CheckForWinner();
            }
        }


        private void CheckForWinner()
        {
            int[,] winConditions = new int[,]
            {
                {0, 1, 2}, {3, 4, 5}, {6, 7, 8}, 
                {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, 
                {0, 4, 8}, {2, 4, 6} 
            };

            for (int i = 0; i < winConditions.GetLength(0); i++)
            {
                int a = winConditions[i, 0], b = winConditions[i, 1], c = winConditions[i, 2];
                if (board[a] == board[b] && board[b] == board[c] && board[a] != 0)
                {
                    EndGame(board[a] == 1 ? "Крестики победили!" : "Нолики победили!");
                    return;
                }
            }

            bool draw = true;
            foreach (var square in board)
            {
                if (square == 0)
                {
                    draw = false;
                    break;
                }
            }

            if (draw)
            {
                EndGame("Ничья!");
            }
        }

        private void EndGame(string result)
        {
            ResultText.Text = result;
            DisableButtons();
        }

        private void DisableButtons()
        {
            for (int i = 0; i < board.Length; i++)
            {
                ((Button)FindName($"Button{i}")).IsEnabled = false;
            }
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            RestartGame();
        }

        private void RestartGame()
        {
            for (int i = 0; i < board.Length; i++)
            {
                board[i] = 0;
                ((Button)FindName($"Button{i}")).Content = string.Empty;
                ((Button)FindName($"Button{i}")).IsEnabled = true;
            }
            isPlayerTurn = true; 
            ResultText.Text = string.Empty;
        }
    }
}
