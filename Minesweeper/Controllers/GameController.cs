using Minesweeper.Interfaces;
using System;

namespace Minesweeper.Controllers
{
    public class GameController : IGameController
    {
        #region private members
        private readonly IBoardManager boardManager;
        private int numberOfMoves = 0;
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="boardManager"></param>
        public GameController(IBoardManager boardManager)
        {
            this.boardManager = boardManager;
        }
        #endregion
        #region Public Methods
        public void StartGame()
        {
            PrintCurrentBoard();

            // Loop through the game until it reaches the last column
            while (boardManager.GetPlayerPosition().Item2 < boardManager.BoardSize - 1)
            {
                Console.Write("Use Right/Left/Up/Down arrows to play the game: ");
                var key = Console.ReadKey().Key;
                if (boardManager.IsPlayerMoveValid(key))
                {
                    MovePlayer(key);
                    PrintCurrentBoard();
                }
                else
                {
                    Console.WriteLine("\nInvalid move! Please enter again.");
                }
            }

            Console.WriteLine($"\nCongratulations! You reached the other side in {numberOfMoves} moves with {boardManager.TotalLives} lives left.");
        }
        #endregion
        #region private Methods
        /// <summary>
        /// Prints the current board to Console
        /// </summary>
        private void PrintCurrentBoard()
        {
            var board = boardManager.Board;
            Console.WriteLine("\nCurrent Board:");
            for (int i = 0; i < boardManager.BoardSize; i++)
            {
                for (int j = 0; j < boardManager.BoardSize; j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine($"Lives: {boardManager.TotalLives}, Moves: {numberOfMoves}");
        }

        /// <summary>
        /// Method to Execute Move Player on Board
        /// </summary>
        /// <param name="key"></param>
        private void MovePlayer(ConsoleKey key)
        {
            var (row, col) = boardManager.GetPlayerPosition();
            switch (key)
            {
                case ConsoleKey.UpArrow: row--; break;
                case ConsoleKey.DownArrow: row++; break;
                case ConsoleKey.LeftArrow: col--; break;
                case ConsoleKey.RightArrow: col++; break;
            }

            bool hitMine = boardManager.Board[row, col] == 'X';
            if (hitMine)
            {
                boardManager.TotalLives--;
                Console.WriteLine($"\nYou hit a mine! {boardManager.TotalLives} lives left to Continue");

                if (boardManager.TotalLives == 0)
                {
                    Console.WriteLine("\nGame over! You ran out of lives.");
                    Environment.Exit(0);
                }
            }

            boardManager.UpdatePlayerPosition(row, col, hitMine);
            numberOfMoves++;
        }
        #endregion
    }
}
