using Connect4;
using System;

namespace Connect4
{
    public class Game
    {
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        public Player CurrentPlayer { get; private set; }

        public Game(Player player1, Player player2)
        {
            Player1 = player1;
            Player2 = player2;
            CurrentPlayer = Player1;
            ResetGame();
        }

        public int[,] Board { get; private set; } = new int[7, 6];

        public int GetAvailableRow(int column)
        {
            for (int row = Board.GetLength(1) - 1; row >= 0; row--)
            {
                if (Board[column, row] == 0)
                {
                    return row;
                }
            }
            return -1; // Column is full
        }

        public void PlayMove(int column)
        {
            int row = GetAvailableRow(column);
            if (row != -1)
            {
                Board[column, row] = CurrentPlayer.Id;
                SwitchPlayer();
                CheckForWin();

               // var newBoard = new int[7, 6];
                //Array.Copy(Board, newBoard, Board.Length);
                //Board = newBoard;
            }
        }

        private void SwitchPlayer()
        {
            CurrentPlayer = CurrentPlayer == Player1 ? Player2 : Player1;
        }

        public bool CheckForWin()
        {
            // Implement win checking logic
            return false;
        }

        public void ResetGame()
        {
            Board = new int[7, 6]; // Reset the board
            CurrentPlayer = Player1;
        }
    }
}