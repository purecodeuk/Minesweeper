using Minesweeper.Managers;
using NUnit.Framework;

namespace Minesweeper.Tests
{
    [TestFixture]
    public class BoardManagerTests
    {
        private BoardManager _boardManager;

        [SetUp]
        public void Setup()
        {
            _boardManager = new BoardManager();
        }

        [Test]
        public void BoardIsProperlyInitialized()
        {
            var board = _boardManager.Board;
            int countMines = 0;
            for (int i = 0; i < _boardManager.BoardSize; i++)
            {
                for (int j = 0; j < _boardManager.BoardSize; j++)
                {
                    if (board[i, j] == 'X')
                        countMines++;
                    if (i == 4 && j == 0)
                        Assert.AreEqual('P', board[i, j]); // Assuming starting position is middle of the first column
                    else if (board[i, j] != 'X')
                        Assert.AreEqual('-', board[i, j]);
                }
            }
            Assert.AreEqual(_boardManager.BoardSize, countMines); // Assuming mines planted are equal to BoardSize
        }

        [Test]
        public void UpdatePlayerPosition_UpdatesPositionCorrectly()
        {
            _boardManager.UpdatePlayerPosition(5, 1, false); // Move player to a new position without hitting a mine
            var (row, col) = _boardManager.GetPlayerPosition();

            Assert.AreEqual(5, row);
            Assert.AreEqual(1, col);
            Assert.AreEqual('P', _boardManager.Board[5, 1]);
            Assert.IsFalse(_boardManager.WasItMine);
        }

        [Test]
        public void UpdatePlayerPosition_MineHitUpdatesCorrectly()
        {
            // Set a mine at a specific location and test hitting it
            var board = _boardManager.Board;
            board[5, 1] = 'X';
            _boardManager.UpdatePlayerPosition(5, 1, true); // Move player to the mine position
            var (row, col) = _boardManager.GetPlayerPosition();

            Assert.AreEqual(5, row);
            Assert.AreEqual(1, col);
            Assert.AreEqual('P', _boardManager.Board[5, 1]);
            Assert.IsTrue(_boardManager.WasItMine);
        }

        [Test]
        public void NoMinesAtStartingPosition()
        {
            var board = _boardManager.Board;
            Assert.AreNotEqual('X', board[4, 0], "There should be no mine at the starting position.");
        }

        [Test]
        public void BoundaryMovement_PreventOutOfBounds()
        {
            _boardManager.UpdatePlayerPosition(0, -1, false); // Attempt to move out of bounds to the left
            var (row, col) = _boardManager.GetPlayerPosition();
            Assert.AreEqual(0, col, "Player should not move out of left boundary.");

            _boardManager.UpdatePlayerPosition(0, 8, false); // Attempt to move out of bounds to the right
            (row, col) = _boardManager.GetPlayerPosition();
            Assert.AreEqual(0, col, "Player should not move out of right boundary.");

            _boardManager.UpdatePlayerPosition(-1, 0, false); // Attempt to move out of bounds upwards
            (row, col) = _boardManager.GetPlayerPosition();
            Assert.AreEqual(4, row, "Player should not move out of top boundary.");

            _boardManager.UpdatePlayerPosition(8, 0, false); // Attempt to move out of bounds downwards
            (row, col) = _boardManager.GetPlayerPosition();
            Assert.AreEqual(4, row, "Player should not move out of bottom boundary.");
        }
    }
}
