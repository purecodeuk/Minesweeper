using System;

namespace Minesweeper.Interfaces
{
    public interface IBoardManager
    {
        char[,] Board { get; }
        int BoardSize { get; }
        bool WasItMine { get; }
        int TotalLives { get; set; }
        (int, int) GetPlayerPosition();
        void UpdatePlayerPosition(int newRow, int newCol, bool hitMine);
        bool IsPlayerMoveValid(ConsoleKey key);
    }
}
