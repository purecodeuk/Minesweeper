using Minesweeper.Interfaces;
using System;

namespace Minesweeper.Managers
{
    public class BoardManager : IBoardManager
    {
        #region private members
        private int boardSize = 8;
        private char[,] board;
        private int playerRow;
        private int playerCol;
        private bool wasItMine;
        private Random random = new Random();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public BoardManager()
        {
            board = new char[boardSize, boardSize];
            InitializeBoard();
        }
        #endregion

        #region Properties
        public char[,] Board => board;
        public int BoardSize => boardSize;
        public bool WasItMine => wasItMine;
        public int TotalLives { get; set; } = 3;
        #endregion

        #region Public Methods
        /// <summary>
        /// Method to Initialise the Board
        /// </summary>
        public void InitializeBoard()
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    board[i, j] = '-';
                }
            }

            // Place player at starting position
            playerCol = 0;
            playerRow = boardSize / 2;
            board[playerRow, playerCol] = 'P';

            // Plant mines at Random places
            for (int i = 0; i < boardSize; i++)
            {
                int rowMine = random.Next(boardSize);
                while (rowMine == playerRow)
                {
                    rowMine = random.Next(boardSize);
                }
                int colMine = random.Next(boardSize);
                board[rowMine, colMine] = 'X';
            }
        }
        /// <summary>
        /// Updated Player Position and Resets the previous position back
        /// </summary>
        /// <param name="newRow"></param>
        /// <param name="newCol"></param>
        /// <param name="hitMine"></param>
        public void UpdatePlayerPosition(int newRow, int newCol, bool hitMine)
        {
            if (newRow >= 0 && newRow < BoardSize && newCol >= 0 && newCol < BoardSize)
            {
                board[playerRow, playerCol] = wasItMine ? 'X' : '-';
                playerRow = newRow;
                playerCol = newCol;
                board[playerRow, playerCol] = 'P';
                wasItMine = hitMine;
            }
        }
        /// <summary>
        /// Method validates whether this move is valid or not
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsPlayerMoveValid(ConsoleKey key)
        {
            var (row, col) = GetPlayerPosition();
            return key switch
            {
                ConsoleKey.UpArrow => row > 0,
                ConsoleKey.DownArrow => row < 7,
                ConsoleKey.LeftArrow => col > 0,
                ConsoleKey.RightArrow => true,
                _ => false
            };
        }
        /// <summary>
        /// Gets the Players Position
        /// </summary>
        /// <returns>Returns the Location of the Player</returns>
        public (int, int) GetPlayerPosition() => (playerRow, playerCol);
        #endregion
    }
}
