using Minesweeper.Controllers;
using Minesweeper.Interfaces;
using Moq;
using NUnit.Framework;

namespace Minesweeper.Tests
{
    [TestFixture]
    public class GameControllerTests
    {
        private GameController _gameController;
        private Mock<IBoardManager> _mockBoardManager;

        [SetUp]
        public void Setup()
        {
            _mockBoardManager = new Mock<IBoardManager>();
            _gameController = new GameController(_mockBoardManager.Object);
            _mockBoardManager.Setup(m => m.BoardSize).Returns(8);
            _mockBoardManager.Setup(m => m.Board).Returns(new char[8, 8]);
        }

        [Test]
        public void GameController_StartGame_ProcessesPlayerMovesUntilGameEnds()
        {
            // Assume the player moves directly to the end without hitting any mines
            _mockBoardManager.Setup(m => m.GetPlayerPosition()).Returns(() => (0, 7)); // Simulate reaching the end           

            Assert.DoesNotThrow(() => _gameController.StartGame()); // Test that the game can start and run without issues
        }
    }
}
